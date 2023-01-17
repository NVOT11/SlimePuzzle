using Common;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Stage
{
    public class StageStateBase : StateBase
    {
        public static EmptyState Empty { get; set; } = new EmptyState();

        public StageContext Context => StageContext.Current;

        public override UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            return UniTask.FromResult<StateBase>(this);
        }
    }
}