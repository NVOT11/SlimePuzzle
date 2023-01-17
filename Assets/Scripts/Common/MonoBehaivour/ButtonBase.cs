using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Common
{
    public class ButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button _button;

        protected virtual void Awake()
        {
            _button.OnSubmitAsObservable().Subscribe(_ => OnSubmit()).AddTo(_button);
            _button.OnClickAsObservable().Subscribe(_ => OnClick()).AddTo(_button);
        }

        public virtual void OnClick()
        {
            //
        }

        public virtual void OnSubmit()
        {
            //
        }
    }
}
