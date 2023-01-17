using Audio;
using Common;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stage
{
    public class FinishState : StageStateBase
    {
        public async override UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            Context.InputEnabled = false;

            AudioManager.Current.PauseBGM();
            AudioManager.Current.PlaySE("Finished");

            var text = StageContext.Current.Messages.GetText("FoundGoal");
            Context.Dialog.ShowMessage(text);

            // セーブしておく
            GameContext.Current.SaveData.SetArchevement(Context.CurrentRank, true, withSave: true);

            Context.VPad.Hide();
            Context.HUD.Hide();

            await UniTask.Delay(2000, cancellationToken: token);

            Context.EndPanel.Show();
            var selection = await Context.EndPanel.WaitForConfirm(token);

            if (selection == EndPanel.Selection.Exit)
            {
                return new ExitState();
            }

            if (selection == EndPanel.Selection.Next)
            {
                if (GameContext.Current.StageRank.IsFinalStageFinished)
                {
                    GameContext.Current.EDFlag = true;
                }
                else
                {
                    GameContext.Current.StageRank.NextRank();
                }

                SceneManager.LoadSceneAsync("Stage");
            }

            return Empty;
        }
    }
}