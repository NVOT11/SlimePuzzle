using Common;
using Cysharp.Threading.Tasks;
using System.Threading;
using Audio;

namespace Stage
{
    public class PlayingState : StageStateBase
    {
        public async override UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            await Context.Slime.ActivateAsync(token);

            if (Context.CurrentRank == 1)
            {
                AudioManager.Current.PlayBGM("Event");
            }
            else
            {
                AudioManager.Current.PlayBGM("Stage");
            }

            Context.InputEnabled = true;

            return Empty;
        }
    }
}