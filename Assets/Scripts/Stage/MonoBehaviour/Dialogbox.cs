using UnityEngine;
using Common;

namespace Stage
{
    public class Dialogbox : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextComponent _text;

        private void Start()
        {
            HideMessage();
        }

        public void ShowMessage(string s)
        {
            _canvasGroup.alpha = 1.0f;
            _text.SetString(s);
        }

        public void HideMessage()
        {
            _canvasGroup.alpha = 0.0f;
        }
    }
}