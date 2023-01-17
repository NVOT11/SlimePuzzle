using Audio;
using Common;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class TitleButton : ButtonBase
    {
        private bool _sumbit;

        public override void OnSubmit()
        {
            if (_sumbit) return;
            _sumbit = true;
            SceneManager.LoadSceneAsync("Title");

            AudioManager.Current.PlaySE("Pop");
        }

        public override void OnClick()
        {
            if (_sumbit) return;
            _sumbit = true;
            SceneManager.LoadSceneAsync("Title");

            AudioManager.Current.PlaySE("Pop");
        }
    }
}