using Cysharp.Threading.Tasks;
using System.Threading;

namespace Common
{
    public abstract class StateBase
    {
        /// <summary>
        /// ステートに入った時の処理
        /// 終わったら次のステートを返す
        /// </summary>
        public abstract UniTask<StateBase> OnEnterState(CancellationToken token = default);
    }
}