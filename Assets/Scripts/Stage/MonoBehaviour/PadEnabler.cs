using UnityEngine;
using Common;

namespace Stage
{
    public class PadEnabler : ButtonBase
    {
        [SerializeField] TextComponent _text;
        [SerializeField] VirtualPad _virtualPad;

        private void Start()
        {
            if (StageContext.Current.IsEnding()) 
            {
                _button.gameObject.SetActive(false);
            }
        }

        public override void OnClick()
        {
            _virtualPad.IsEnabled = !_virtualPad.IsEnabled;

            if (_virtualPad.IsEnabled)
            {
                _text.SetString("パッドON");
                _virtualPad.Show();
                GameContext.Current.SettingData.UseVirtualPad = true;
            }
            else
            {
                _text.SetString("パッドOFF");
                _virtualPad.Hide();
                GameContext.Current.SettingData.UseVirtualPad = false;
            }
            GameContext.Current.SettingData.SaveVirtualPadSetting();
        }
    }
}