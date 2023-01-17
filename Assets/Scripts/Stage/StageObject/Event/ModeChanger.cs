using Audio;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Stage
{
    public class ModeChanger : GridObjectBase, IInteractable
    {
        /// <summary>
        /// 変更先モード
        /// </summary>
        [SerializeField] private SlimeMode _slimeMode;

        public int InteractionPriority => INTERACTABLE.JUWEL;

        public UniTask OnInteractAsync(CancellationToken token = default)
        {
            // 同じモードならスルー
            if (_slimeMode == StageContext.Current.Slime.Mode) return UniTask.CompletedTask;
            
            AudioManager.Current.PlaySE("ModeChange");
            StageContext.Current.Slime.ModeChange(_slimeMode);

            StageContext.Current.RxSlimeMode.Value = _slimeMode;
            return UniTask.CompletedTask;
        }
    }
}
