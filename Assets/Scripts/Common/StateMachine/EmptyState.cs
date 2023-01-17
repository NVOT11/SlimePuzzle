using Cysharp.Threading.Tasks;
using System.Threading;

namespace Common
{
    public class EmptyState : StateBase
    {
        public override async UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
            return this;
        }
    }
}