using Audio;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Menu
{
    public class StageButton : ButtonBase
    {
        public int Rank => _rank;
        public GameObject ButtonObject => _button.gameObject;

        [SerializeField] protected int _rank = 1;

        [SerializeField] private Image _archevedIcon;

        private bool _sumbit;

        private void Start()
        {
            var stageName = GameContext.Current.GetStageName(_rank);

            var tex = ButtonObject.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            tex.text = stageName;

            var archeved = GameContext.Current.SaveData.IsArcheved(_rank);
            _archevedIcon.enabled = archeved;
        }

        public override void OnSubmit()
        {
            if (_sumbit) return;
            _sumbit = true;

            AudioManager.Current.PlaySE("Enter");

            GameContext.Current.StageRank.SetRank(_rank);

            SceneManager.LoadSceneAsync("Stage");
        }

        public override void OnClick()
        {
            if (_sumbit) return;
            _sumbit = true;

            AudioManager.Current.PlaySE("Enter");

            GameContext.Current.StageRank.SetRank(_rank);

            SceneManager.LoadSceneAsync("Stage");
        }
    }
}