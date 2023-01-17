using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Save
{
    public class SettingData
    {
        //
        public float BGMVolume = 0.8f;
        public float SEVolume = 0.8f;

        public bool UseVirtualPad = false;

        public void SaveBGMVolume()
        {
            PlayerPrefs.SetFloat(Key_BGM, BGMVolume);
        }

        public void SaveSEVolume()
        {
            PlayerPrefs.SetFloat(Key_SE, SEVolume);
        }

        public void SaveVirtualPadSetting()
        {
            PlayerPrefs.SetString(Key_VirtualPad, UseVirtualPad.ToString());
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(Key_BGM)) BGMVolume = PlayerPrefs.GetFloat(Key_BGM, 0.8f);
            if (PlayerPrefs.HasKey(Key_SE)) SEVolume = PlayerPrefs.GetFloat(Key_SE, 0.8f);

            if (PlayerPrefs.HasKey(Key_VirtualPad))
            {
                var result = PlayerPrefs.GetString(Key_VirtualPad, "false");
                UseVirtualPad = result.ToLower() == "true".ToLower() ? true : false;
            }
        }

        private static string Key_BGM => nameof(BGMVolume);
        private static string Key_SE => nameof(SEVolume);
        private static string Key_VirtualPad => nameof(UseVirtualPad);
    }

    public class SaveData
    {
        /// <summary>
        /// 一括セーブ
        /// </summary>
        public void Save()
        {
            for (int i = 1; i <= 11; i++)
            {
                var key = $"Rank{i}";

                var archeved = IsArcheved(i);
                SetArchevement(i, archeved);

                // 実際にセーブ
                PlayerPrefs.SetString(key, archeved.ToString());
            }
        }

        /// <summary>
        /// 一括ロード
        /// </summary>
        public void Load()
        {
            for (int i = 1; i <= 11; i++)
            {
                var key = $"Rank{i}";

                // データがなければスルー
                if (!PlayerPrefs.HasKey(key)) continue;

                var archeved = PlayerPrefs.GetString(key);
                var result = archeved.ToLower() == "true".ToLower() ? true : false;

                // 現在データに反映
                SetArchevement(i, result, withSave: false);
            }
        }

        public bool Rank1;
        public bool Rank2;
        public bool Rank3;
        public bool Rank4;
        public bool Rank5;
        public bool Rank6;
        public bool Rank7;
        public bool Rank8;
        public bool Rank9;
        public bool Rank10;
        public bool Rank11;

        /// <summary>
        /// 値を取得
        /// </summary>
        public bool IsArcheved(int rank)
        {
            return rank switch
            {
                1 => Rank1,
                2 => Rank2,
                3 => Rank3,
                4 => Rank4,
                5 => Rank5,
                6 => Rank6,
                7 => Rank7,
                8 => Rank8,
                9 => Rank9,
                10 => Rank10,
                11 => Rank11,
                _ => false,
            };
        }

        /// <summary>
        /// 値をセット
        /// オプションでついでにセーブする
        /// </summary>
        public void SetArchevement(int rank, bool boolean, bool withSave = false)
        {
            var _ = rank switch
            {
                1 => Rank1 = boolean,
                2 => Rank2 = boolean,
                3 => Rank3 = boolean,
                4 => Rank4 = boolean,
                5 => Rank5 = boolean,
                6 => Rank6 = boolean,
                7 => Rank7 = boolean,
                8 => Rank8 = boolean,
                9 => Rank9 = boolean,
                10 => Rank10 = boolean,
                11 => Rank11 = boolean,
                _ => false,
            };

            if (withSave)
            {
                var key = $"Rank{rank}";
                PlayerPrefs.SetString(key, boolean.ToString());
            }
        }
    }
}