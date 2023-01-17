using Audio;
using Common;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Stage
{
    public class RetryState : StageStateBase
    {
        public async override UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            Context.InputEnabled = false;

            AudioManager.Current.PlaySE("Start");

            SceneManager.LoadSceneAsync("Stage");

            return Empty;
        }
    }
}