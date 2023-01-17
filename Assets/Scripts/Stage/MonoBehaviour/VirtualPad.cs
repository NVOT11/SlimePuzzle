using UnityEngine;
using Common;

namespace Stage
{

    public class VirtualPad : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;

        public bool IsEnabled;

        private void Start()
        {
            var use = GameContext.Current.SettingData.UseVirtualPad;

            if (use)
            {
                IsEnabled = true;
                Show();
            }
            else
            {
                IsEnabled = false;
                Hide();
            }
        }

        public void Show()
        {
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}