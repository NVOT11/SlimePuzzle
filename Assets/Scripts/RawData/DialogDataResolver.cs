using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.StaticData
{
    public class DialogDataResolver
    {
        public static DialogText[] GetTexts(TextAsset textAsset)
        {
            var rawData = JsonUtility.FromJson<RawMasterData>(textAsset.ToString());
            return rawData.DialogTexts;
        }

        /// <summary>
        /// データを一旦受ける為に使用
        /// </summary>
        [Serializable]
        public class RawMasterData
        {
            // 変数名をJSONと一致させる
            public DialogText[] DialogTexts;
        }
    }
}