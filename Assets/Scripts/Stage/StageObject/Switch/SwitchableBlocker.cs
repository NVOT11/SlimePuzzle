using UnityEngine;
using Audio;

namespace Stage
{
    /// <summary>
    /// スイッチで切り替える
    /// デフォルトで、スイッチオン＝出現
    /// </summary>
    public class SwitchableBlocker : GridObjectBase, ITraversable, ISwitchable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private bool _reverse = false;

        public int InteractionPriority => 2;
        public int TravasalPriority => TRAVASAL.OBSTACLE;

        public Traversal Travase => _traversal;
        private Traversal _traversal = Traversal.Forbidden;

        private void Start()
        {
            if (_reverse) Switched(true);
        }

        public void OnSwitchChanged(bool value)
        {
            // True = スイッチオン
            var v = _reverse ? !value : value;
            Switched(v);
        }

        private void Switched(bool enabled)
        { 
            // Trueならスプライトを表示して通行禁止になる
            _spriteRenderer.enabled = !enabled;
            _traversal = !enabled ? Traversal.Forbidden : Traversal.CanEnter;

            AudioManager.Current.PlaySE("Noise");
        }
    }
}
