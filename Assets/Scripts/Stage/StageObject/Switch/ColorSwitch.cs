using Audio;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Stage
{
    public class ColorSwitch : GridObjectBase, IInteractable
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] Sprite _enableSprite;
        [SerializeField] Sprite _disableSprite;

        public int InteractionPriority => INTERACTABLE.SWTICH;

        // 対応モード
        [SerializeField] private SlimeMode _slimeMode;
        // 完了通知用
        public UniTaskCompletionSource Completion = new UniTaskCompletionSource();

        private void Awake()
        {
            _spriteRenderer.sprite = _enableSprite;
        }

        public async UniTask OnInteractAsync(CancellationToken token = default)
        {
            // 対応モードでない場合はスルー
            if (_slimeMode != StageContext.Current.Slime.Mode) return;

            Completion.TrySetResult();
            _spriteRenderer.sprite = _disableSprite;

            AudioManager.Current.PlaySE("Switch");

            await UniTask.Delay(100, cancellationToken: token);
        }
    }
}
