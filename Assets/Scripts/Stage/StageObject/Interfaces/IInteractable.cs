using Cysharp.Threading.Tasks;
using System.Threading;

namespace Stage
{
    /// <summary>
    /// 調査できるオブジェクトに実装する
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// 優先度の高いものを参照する
        /// </summary>
        int InteractionPriority { get; }

        /// <summary>
        /// 実行内容
        /// </summary>
        public UniTask OnInteractAsync(CancellationToken token = default);

        /// <summary>
        /// 離れた時
        /// 何もしないことの方が多い
        /// </summary>
        public UniTask OnExitAsync(CancellationToken token = default) => UniTask.CompletedTask;
    }
}
