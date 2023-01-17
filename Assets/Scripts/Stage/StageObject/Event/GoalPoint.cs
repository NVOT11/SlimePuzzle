using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// ゴール位置
    /// </summary>
    public class GoalPoint : GridObjectBase, IInteractable
    {
        public int InteractionPriority => INTERACTABLE.GOAL;

        public UniTask OnInteractAsync(CancellationToken token = default)
        {
            StageContext.Current.StateMachine.SetState(new FinishState());
            return UniTask.CompletedTask;
        }
    }
}
