using Audio;
using Common;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TitleScene
{
    public class SESlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void Start()
        {
            _slider.value = GameContext.Current.SettingData.SEVolume;

            _slider.OnValueChangedAsObservable()
                .Subscribe(_ => OnValueChanged(_))
                .AddTo(this);
        }

        private void OnValueChanged(float value)
        {
            var moderated = Mathf.RoundToInt(value * 100) * 0.01f;

            AudioManager.Current.VolumeController.SetSEVolume(moderated);
            AudioManager.Current.VolumeController.SetActualSourceVolume();

            GameContext.Current.SettingData.SEVolume = moderated;
            GameContext.Current.SettingData.SaveSEVolume();
        }
    }

}
