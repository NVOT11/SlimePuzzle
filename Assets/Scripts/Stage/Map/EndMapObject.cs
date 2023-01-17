using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using Common.Animation;

namespace Stage
{
    public class EndMapObject : MapObject
    {
        [SerializeField] private Transform _player;
        [SerializeField] private SimpleAsyncAnimator _animator;

        public EndMapObject CreateMap()
        {
            return GameObject.Instantiate(this);
        }

        public async UniTask PlayEDAsync(CancellationToken token = default)
        {
            StageContext.Current.FollowCamera.SetTarget(_player);
            StageContext.Current.FollowCamera.Run(this.gameObject);

            await UniTask.Delay(500, cancellationToken: token);

            var currentY = _player.position.y;
            await _player.DOMoveY(currentY + 4.0f, 2.0f).SetSpeedBased().WithCancellation(token);

            await UniTask.Delay(500, cancellationToken: token);

            var text = StageContext.Current.Messages.GetText("BackHome");
            StageContext.Current.Dialog.ShowMessage(text);

            await UniTask.Delay(2000, cancellationToken: token);

            StageContext.Current.Dialog.HideMessage();

            var currentY2 = _player.position.y;
            await _player.DOMoveY(currentY2 + 1.0f, 0.6f).SetSpeedBased().WithCancellation(token);

            await UniTask.Delay(500, cancellationToken: token);

            _animator.Cancel(false);

            await UniTask.Delay(500, cancellationToken: token);

            var end = StageContext.Current.Messages.GetText("END");
            StageContext.Current.Dialog.ShowMessage(end);

            await UniTask.Delay(2400, cancellationToken: token);
        }
    }
}