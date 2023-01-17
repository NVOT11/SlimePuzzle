using Common;
using UniRx;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Stage
{
    public class SlimeController
    {
        // Shortcutr
        private bool InputEnabled => StageContext.Current.InputEnabled;

        public void Run(GameObject token)
        {
            InputManager.InputMove
                .Where(_ => InputEnabled)
                .Subscribe(_ => Move(_))
                .AddTo(token);

            InputManager.Key_1
                .Where(_ => InputEnabled)
                .Subscribe(_ => KeyA(_))
                .AddTo(token);

            InputManager.Key_2
                .Where(_ => InputEnabled)
                .Subscribe(_ => KeyB(_))
                .AddTo(token);

            InputManager.Key_Reset
                .Where(_ => InputEnabled)
                .Subscribe(_ => StageReset(_))
                .AddTo(token);

            InputManager.Key_Exit
                .Where(_ => InputEnabled)
                .Subscribe(_ => Exit(_))
                .AddTo(token);
        }

        private void KeyA(bool _)
        {
            StageContext.Current.Slime.Duplicate();
        }

        private void KeyB(bool _)
        {
            StageContext.Current.SlimeList.NextSlime();
        }

        private void Move(Direction direction)
        {
            if (direction == Direction.None) return;
            StageContext.Current.Slime.SetDirection(direction);
            StageContext.Current.Slime.Move();
        }

        private void StageReset(bool _)
        {
            StageContext.Current.StateMachine.SetState(new RetryState());
        }

        private void Exit(bool _)
        {
            StageContext.Current.StateMachine.SetState(new ExitState());
        }
    }
}
