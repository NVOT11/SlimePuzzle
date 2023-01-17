using Audio;
using Common;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Stage
{
    public class HoldSwitcher : GridObjectBase, IInteractable
    {
        [SerializeField] Sprite _holded;
        [SerializeField] Sprite _released;

        [SerializeField] GameObject[] _switchables;

        public int InteractionPriority => INTERACTABLE.SWTICH;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();
        }

        public async UniTask OnInteractAsync(CancellationToken token = default)
        {
            _spriteRenderer.sprite = _holded;

            foreach (var switchable in _switchables)
            {
                switchable.GetComponent<ISwitchable>()?.OnSwitchChanged(true);
            }

            AudioManager.Current.PlaySE("Switch");

            await UniTask.Delay(100, cancellationToken: token);
        }

        public async UniTask OnExitAsync(CancellationToken token = default)
        {
            _spriteRenderer.sprite = _released;

            foreach (var switchable in _switchables)
            {
                switchable.GetComponent<ISwitchable>()?.OnSwitchChanged(false);
            }

            AudioManager.Current.PlaySE("Switch");

            await UniTask.Delay(100, cancellationToken: token);
        }
    }
}
