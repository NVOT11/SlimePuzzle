using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using UniRx;

namespace Stage
{
    public class FloatingFloorSwitched : GridObjectBase, ITraversable, ISwitchable
    {
        // 乗り込める
        public int TravasalPriority => TRAVASAL.OVERFLOOR;
        public Traversal Travase => Traversal.CanEnter;

        // 移動スピード
        [SerializeField] private float _speed = 1.0f;

        // 移動先
        [SerializeField] private Vector3 _movement;
        private Vector3 _defaultPostion;

        private void Awake()
        {
            // 初期位置セット
            _defaultPostion = this.transform.position;
        }

        // 移動中にスイッチを切り替えた場合はキャンセル
        CancellationTokenSource _CTS = new CancellationTokenSource();

        private void Cancel()
        {
            _CTS.Cancel();
            _CTS = new CancellationTokenSource();
            _CTS.AddTo(this);
        }

        public void OnSwitchChanged(bool value)
        {
            Cancel();
            if (value) SwitchOnAsync(_CTS.Token).Forget();
            else SwitchOffAsync(_CTS.Token).Forget();
        }

        private async UniTask SwitchOnAsync(CancellationToken token = default)
        {
            var hovered = MapManager.CheckSlime(this.transform.position, Direction.None);

            var goal = _defaultPostion + _movement;

            if (!hovered)
            {
                await this.transform.DOMove(goal, _speed).SetSpeedBased().WithCancellation(token);
                return;
            }
            else
            {
                hovered.VirtualSlime.SetLock(true);

                var tw1 = this.transform.DOMove(goal, _speed).SetSpeedBased()
                    .WithCancellation(token);

                var tw2 = hovered.transform.DOMove(goal, _speed).SetSpeedBased()
                    .WithCancellation(token);

                await UniTask.WhenAll(tw1, tw2);

                hovered.VirtualSlime.SetLock(false);
            }
        }

        private async UniTask SwitchOffAsync(CancellationToken token = default)
        {
            var hovered = MapManager.CheckSlime(this.transform.position, Direction.None);

            if (!hovered)
            {
                await this.transform.DOMove(_defaultPostion, _speed).SetSpeedBased().WithCancellation(token);
                return;
            }
            else
            {
                hovered.VirtualSlime.SetLock(true);

                var tw1 = this.transform.DOMove(_defaultPostion, _speed).SetSpeedBased()
                    .WithCancellation(token);

                var tw2 = hovered.transform.DOMove(_defaultPostion, _speed).SetSpeedBased()
                    .WithCancellation(token);

                await UniTask.WhenAll(tw1, tw2);

                hovered.VirtualSlime.SetLock(false);
            }
        }
    }
}
