using UnityEngine.SceneManagement;
using Common;
using Audio;

namespace TitleScene
{
    public class GameStartButton : ButtonBase 
    {
        private bool _submit;

        public override void OnSubmit()
        {
            if (_submit) return;
            _submit = true;

            AudioManager.Current.PlaySE("Start");

            SceneManager.LoadSceneAsync("Menu");
        }

        public override void OnClick()
        {
            if (_submit) return;
            _submit = true;

            AudioManager.Current.PlaySE("Start");

            SceneManager.LoadSceneAsync("Menu");
        }
    }
}
