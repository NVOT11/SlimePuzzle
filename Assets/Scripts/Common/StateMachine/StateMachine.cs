using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace Common
{
    public class StateMachine : IDisposable 
    {
        /// <summary>
        /// 現在ステート
        /// </summary>
        public StateBase CurrentState { get; private set; } = new EmptyState();

        /// <summary>
        /// Tは現在ステートと一致するか
        /// </summary>
        public bool IsCurrentState<TState>() where TState : StateBase
            => CurrentState.GetType() == typeof(TState);

        /// <summary>
        /// ステート実行時のCancellationToken
        /// </summary>
        private CancellationTokenSource tokenSource { get; set; } = new CancellationTokenSource();
        private CancellationToken token => tokenSource.Token;

        /// <summary>
        /// StateMachineを起動する
        /// 外からは呼ばない
        /// </summary>
        private async UniTaskVoid Run()
        {
            while (!token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();

                CurrentState = await CurrentState.OnEnterState(token);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }

        /// <summary>
        /// ステートクラスをセットし、実行する
        /// </summary>
        public void SetState(StateBase state, bool cancelState = true)
        {
            // 現在ステートをキャンセル
            if (cancelState) CancelState();
            CurrentState = state;
            Run().Forget();
        }

        /// <summary>
        /// 実行中ステートをキャンセルする
        /// </summary>
        public void CancelState(bool setNewCTS = true)
        {
            tokenSource?.Cancel();
            if (setNewCTS) tokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            CancelState(false);
        }
    }
}