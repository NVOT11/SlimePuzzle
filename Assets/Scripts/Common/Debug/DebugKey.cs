using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Stage;

namespace Common.Debugging
{
    public class DebugKey : MonoBehaviour
    {
        [SerializeField] private bool _enabled = true;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => _enabled)
                .Subscribe(_ => ManagedUpdate())
                .AddTo(this);
        }

        private void ManagedUpdate()
        {
            if (KEY1) EnterStage(1);
            if (KEY2) EnterStage(2);
            if (KEY3) EnterStage(3);
            if (KEY4) EnterStage(4);
            if (KEY5) EnterStage(5);
            if (KEY6) EnterStage(6);
            if (KEY7) EnterStage(7);
            if (KEY8) EnterStage(8);
            if (KEY9) EnterStage(9);
            if (KEY10) EnterStage(10);
            if (KEY11) EnterStage(11);

            //if (S_KEY) GameContext.Current.SaveData.Save();
            //if (L_KEY) GameContext.Current.SaveData.Load();

            if (Q_KEY) StageContext.Current.StateMachine.SetState(new FinishState());
        }

        private void EnterStage(int rank)
        {
            GameContext.Current.StageRank.SetRank(rank);
            SceneManager.LoadScene("Stage");
        }

        private bool KEY1 => Keyboard.current.digit1Key.wasPressedThisFrame;
        private bool KEY2 => Keyboard.current.digit2Key.wasPressedThisFrame;
        private bool KEY3 => Keyboard.current.digit3Key.wasPressedThisFrame;
        private bool KEY4 => Keyboard.current.digit4Key.wasPressedThisFrame;
        private bool KEY5 => Keyboard.current.digit5Key.wasPressedThisFrame;
        private bool KEY6 => Keyboard.current.digit6Key.wasPressedThisFrame;
        private bool KEY7 => Keyboard.current.digit7Key.wasPressedThisFrame;
        private bool KEY8 => Keyboard.current.digit8Key.wasPressedThisFrame;
        private bool KEY9 => Keyboard.current.digit9Key.wasPressedThisFrame;
        private bool KEY10 => Keyboard.current.digit0Key.wasPressedThisFrame;
        private bool KEY11 => Keyboard.current.pKey.wasPressedThisFrame;

        private bool S_KEY => Keyboard.current.sKey.wasPressedThisFrame;
        private bool L_KEY => Keyboard.current.lKey.wasPressedThisFrame;
        private bool Q_KEY => Keyboard.current.qKey.wasPressedThisFrame;
    }
}