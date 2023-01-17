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
        /// �f�[�^����U�󂯂�ׂɎg�p
        /// </summary>
        [Serializable]
        public class RawMasterData
        {
            // �ϐ�����JSON�ƈ�v������
            public DialogText[] DialogTexts;
        }
    }
}