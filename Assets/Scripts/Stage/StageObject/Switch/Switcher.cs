using Audio;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// レバー型のスイッチ
    /// </summary>
    public class Switcher : GridObjectBase, IInteractable
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] Sprite _enableSprite;
        [SerializeField] Sprite _disableSprite;
        [SerializeField] GameObject[] _switchables;
        [SerializeField] bool _initial = false;

        public int InteractionPriority => INTERACTABLE.SWTICH;
        private bool _isOn;

        private void Awake()
        {
            _isOn = _initial;
            _spriteRenderer.sprite = _initial ? _enableSprite : _disableSprite;
        }

        public async UniTask OnInteractAsync(CancellationToken token = default)
        {
            _isOn = !_isOn;
            Changed(_isOn);

            await UniTask.Delay(100, cancellationToken: token);
        }

        public void Changed(bool enable)
        {
            _spriteRenderer.sprite = enable ? _enableSprite : _disableSprite;
            foreach (var switchable in _switchables)
            {
                switchable.GetComponent<ISwitchable>()?.OnSwitchChanged(enable);
            }

            AudioManager.Current.PlaySE("Switch");            
        }
    }
}
