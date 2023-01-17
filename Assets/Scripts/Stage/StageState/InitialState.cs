using Common;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Stage
{
    public class InitialState : StageStateBase
    {
        public async override UniTask<StateBase> OnEnterState(CancellationToken token = default)
        {
            // EDフラグ
            if (Context.IsEnding()) return new EndingState();

            var index = GameContext.Current.CurrentRank - 1;
            var target = index % Context.Maps.Length;

            var currentMap = Context.Maps[target].Create();

            var point = currentMap.SpawnPoint.transform.position;
            var primeSlime = SlimeGanerator.InitialGenarate(point);

            Context.FollowCamera.SetTarget(primeSlime.Transform);
            Context.FollowCamera.Run(Context.SceneToken);

            return new PlayingState();
        }
    }
}