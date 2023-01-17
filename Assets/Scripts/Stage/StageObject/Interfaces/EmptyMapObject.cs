using Cysharp.Threading.Tasks;
using System.Threading;

namespace Stage
{
    /// <summary>
    /// 何もない時に生成される
    /// </summary>
    public class EmptyMapObject : IInteractable
    {
        public int InteractionPriority { get; } = INTERACTABLE.OTHER;

        public UniTask OnInteractAsync(CancellationToken token = default)
        {
            return UniTask.CompletedTask;
        }
    }
}
