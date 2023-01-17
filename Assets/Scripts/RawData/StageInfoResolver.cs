using System;
using UnityEngine;
using System.Linq;

namespace Common.StaticData
{
    public class StageInfoResolver
    {
        public static StageInfo[] GetTexts(TextAsset textAsset)
        {
            var rawData = JsonUtility.FromJson<RawStageInfo>(textAsset.ToString());
            return rawData.StageInfos;
        }

        /// <summary>
        /// データを一旦受ける為に使用
        /// </summary>
        [Serializable]
        public class RawStageInfo
        {
            // 変数名をJSONと一致させる
            public StageInfo[] StageInfos;
        }
    }
}