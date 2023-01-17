using Audio;
using Common;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Stage
{
    /// <summary>
    /// ED再生
    /// </summary>
    public class EndingState : StageStateBase
    {
        public async override UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            GameContext.Current.EDFlag = false;

            var endingStage = GameContext.Current.Setting.EndMapObject.CreateMap();

            // HUDやバーチャルパッドは隠す
            Context.VPad.Hide();
            Context.HUD.Hide();

            AudioManager.Current.PlayBGM("Event");

            // ED再生
            await endingStage.PlayEDAsync(token);

            SceneManager.LoadSceneAsync("Menu");

            return Empty;
        }
    }
}