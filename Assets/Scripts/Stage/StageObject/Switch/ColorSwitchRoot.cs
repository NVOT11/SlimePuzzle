using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniRx;

namespace Stage
{
    public class ColorSwitchRoot : MonoBehaviour
    {
        [SerializeField] ColorSwitch[] _others;
        [SerializeField] GameObject[] _switchables;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            WaitForCompletion(token).Forget();
        }

        async UniTask WaitForCompletion(CancellationToken token)
        {
            var tasks = _others
                .Select(async _ => await _.Completion.Task.AttachExternalCancellation(token));
            await UniTask.WhenAll(tasks);
            Changed(true);
        }

        public void Changed(bool enable)
        {
            foreach (var switchable in _switchables)
            {
                switchable.GetComponent<ISwitchable>()?.OnSwitchChanged(enable);
            }
        }
    }
}
