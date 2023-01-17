using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Threading;

namespace Stage
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;

        [SerializeField] GameObject _nextButton;
        [SerializeField] GameObject _exitButton;

        private void Start()
        {
            Hide();
        }
        public void Hide()
        {
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        public void Show()
        {
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            EventSystem.current.SetSelectedGameObject(_nextButton);
        }

        public async UniTask<Selection> WaitForConfirm(CancellationToken token)
        {
            var submitNext = _nextButton.GetAsyncSubmitTrigger().OnSubmitAsync(token);
            var clickNext = _nextButton.GetAsyncPointerClickTrigger().OnPointerClickAsync(token);

            var submitExit = _exitButton.GetAsyncSubmitTrigger().OnSubmitAsync(token);
            var clickExit = _exitButton.GetAsyncPointerClickTrigger().OnPointerClickAsync(token);

            var result = await UniTask.WhenAny(submitNext, clickNext, submitExit, clickExit);
            var index = result.winArgumentIndex;

            if (index == 0 || index == 1) return Selection.Next;
            if (index == 2 || index == 3) return Selection.Exit;
            return Selection.Next;
        }

        public enum Selection { Next, Exit, }
    }
}