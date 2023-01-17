using Audio;
using Common;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Stage
{
    public class ExitState : StageStateBase
    {
        public async override UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            Context.InputEnabled = false;

            AudioManager.Current.PlaySE("Exit");

            SceneManager.LoadSceneAsync("Menu");

            return Empty;
        }
    }
}