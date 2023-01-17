using UnityEngine;
using Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UniRx;

namespace Stage
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextComponent _floorName;

        public void Show() => _canvasGroup.alpha = 1.0f;
        public void Hide() => _canvasGroup.alpha = 0.0f;

        public void SetFloorName(string str)
        {
            _floorName.SetString(str);
        }

        //
        [SerializeField] private Image[] _icons;

        private void Start()
        {
            StageContext.Current.RxSlimeMode
                .Subscribe(_ => OnValueChanged(StageContext.Current.SlimeList.Slimes))
                .AddTo(this);

            StageContext.Current.SlimeList.OnListChanged
                .Subscribe(_ => OnValueChanged(_))
                .AddTo(this);
        }

        public void OnValueChanged(List<Slime> slimes)
        {
            foreach (var icon in _icons)
            {
                icon.color = Color.black;
            }

            var last = slimes.Count - 1;
            for (int i = last; i >= 0; i--)
            {
                var mode = slimes[i].Mode;
                var sprite = GetSprite(mode);

                _icons[i].sprite = sprite;
                _icons[i].color = Color.white;
            }

            Sprite GetSprite(SlimeMode mode) => mode switch
            {
                SlimeMode.Green => GameContext.Current.Setting.Icons[0],
                SlimeMode.Red => GameContext.Current.Setting.Icons[1],
                SlimeMode.Blue => GameContext.Current.Setting.Icons[2],
                _ => GameContext.Current.Setting.Icons[0]
            };
        }
    }
}