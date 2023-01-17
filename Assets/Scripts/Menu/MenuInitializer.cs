using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Audio;
using Common;
using System.Linq;

namespace Menu
{
    public class MenuInitializer : MonoBehaviour
    {
        [SerializeField] StageButton[] _buttons;
        [SerializeField] GameObject _first;
        [SerializeField] CanvasGroup _fill;

        private void Awake()
        {
            _fill.alpha = 1.0f;
            _fill.blocksRaycasts = true;

            var neverPlayed = !GameContext.Current.SaveData.Rank1;
            if (neverPlayed) 
            {
                SceneManager.LoadScene("Stage");
            }

            if (AudioManager.Current.PlayingBGMName != "Menu")
            {
                AudioManager.Current.PlayBGM("Menu");
            }

            _fill.alpha = 0.0f;
            _fill.blocksRaycasts = false;

            SetFirstSelection();
        }

        private void SetFirstSelection()
        {
            var vp = GameContext.Current.SettingData.UseVirtualPad;

            if (vp)
            {
                EventSystem.current.SetSelectedGameObject(null);

            }
            else
            {
                var rank = GameContext.Current.CurrentRank;
                var current = _buttons.Where(_ => _.Rank == rank).FirstOrDefault();
                if (current != null)
                {
                    EventSystem.current.SetSelectedGameObject(current.ButtonObject);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(_first);
                }
            }
        }
    }
}