using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Threading;
using Audio;
using Common;
using UniRx.Triggers;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace TitleScene
{
    public class TitleSceneInitializer : MonoBehaviour
    {
        [SerializeField] CanvasGroup _initial;
        [SerializeField] GameObject _startButton;

        private void Awake()
        {
            var token = this.GetCancellationTokenOnDestroy();
            InitalizeAsync(token).Forget();
        }

        private async UniTaskVoid InitalizeAsync(CancellationToken token)
        {
            var bgmVolume = GameContext.Current.SettingData.BGMVolume;
            AudioManager.Current.VolumeController.SetBGMVolume(bgmVolume);

            var seVolume = GameContext.Current.SettingData.SEVolume;
            AudioManager.Current.VolumeController.SetSEVolume(seVolume);

            await this.StartAsync();
            AudioManager.Current.VolumeController.SetActualSourceVolume();

            var neverPlayed = !GameContext.Current.SaveData.Rank1;
            var debugChedck = GameContext.Current.Debug.Check;
            if (neverPlayed || debugChedck)
            {
                _initial.alpha = 1.0f;
                _initial.blocksRaycasts = true;

                var anyKeyPressed = UniTask.WaitUntil(() => Keyboard.current.anyKey.wasPressedThisFrame);
                var anyClick = _initial.gameObject.GetAsyncPointerClickTrigger().OnPointerClickAsync();

                var result = await UniTask.WhenAny(anyKeyPressed, anyClick).AttachExternalCancellation(token);

                await UniTask.Delay(200, cancellationToken: token);

                _initial.alpha = 0.0f;
                _initial.blocksRaycasts = false;

                GameContext.Current.SettingData.UseVirtualPad = result == 1 ? true : false;
                GameContext.Current.SettingData.SaveVirtualPadSetting();
            }
            else
            {
                _initial.alpha = 0.0f;
                _initial.blocksRaycasts = false;
            }

            var vp = GameContext.Current.SettingData.UseVirtualPad;
            EventSystem.current.SetSelectedGameObject(vp ? null : _startButton);

            // フォーカスまで待機
            await UniTask.WaitUntil(() => Application.isFocused, cancellationToken: token);

            if (AudioManager.Current.PlayingBGMName != "Menu")
            {
                AudioManager.Current.PlayBGM("Menu");
            }
        }
    }

}
