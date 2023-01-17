using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine;
using System.Collections.Generic;
using Stage;
using UnityEngine.InputSystem;

namespace Common
{
    public class InputManager
    {
        private static CustomKeyBind _keyBind = new CustomKeyBind();

        public static IObservable<Direction> InputMove => _inputMove;
        private static Subject<Direction> _inputMove = new Subject<Direction>();

        public static IObservable<bool> Key_1 => _key_1;
        private static Subject<bool> _key_1 = new Subject<bool>();

        public static IObservable<bool> Key_2 => _key_2;
        private static Subject<bool> _key_2 = new Subject<bool>();

        public static IObservable<bool> Key_Reset => _key_reset;
        private static Subject<bool> _key_reset = new Subject<bool>();

        public static IObservable<bool> Key_Exit => _key_Exit;
        private static Subject<bool> _key_Exit = new Subject<bool>();

        public static void Run(GameObject token)
        {
            _keyBind.Enable();

            _keyBind.Player.Action_A.started += Action_A_started;
            _keyBind.Player.Action_B.started += Action_B_started;
            _keyBind.Player.Reset.started += Reset_started;
            _keyBind.Player.Exit.started += Exit_started;

            token.UpdateAsObservable()
                .Subscribe(_=> MoveInput()).AddTo(token);
        }

        private static void MoveInput()
        {
            var value = _keyBind.Player.Move.ReadValue<Vector2>();
            _inputMove.OnNext(value.ToDirection());
        }

        private static void Action_A_started(InputAction.CallbackContext obj)
        {
            _key_1.OnNext(true);
        }

        private static void Action_B_started(InputAction.CallbackContext obj)
        {
            _key_2.OnNext(true);
        }

        private static void Reset_started(InputAction.CallbackContext obj)
        {
            _key_reset.OnNext(true);
        }

        private static void Exit_started(InputAction.CallbackContext obj)
        {
            _key_Exit.OnNext(true);
        }
    }
}