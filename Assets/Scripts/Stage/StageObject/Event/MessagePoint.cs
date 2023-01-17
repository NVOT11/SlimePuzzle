using Audio;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Common;

namespace Stage
{
    /// <summary>
    /// メッセージイベント
    /// </summary>
    public class MessagePoint : GridObjectBase, IInteractable
    {
        [SerializeField] private string _id;
        [SerializeField] private string _sound;
        [SerializeField] private bool _altForMobile = false;
        [SerializeField] private string _id_alt;

        public int InteractionPriority => INTERACTABLE.DIALOGPOINT;

        public async UniTask OnInteractAsync(CancellationToken token = default)
        {
            if (!string.IsNullOrWhiteSpace(_sound)) AudioManager.Current.PlaySE(_sound);

            if (_altForMobile && GameContext.Current.SettingData.UseVirtualPad)
            {
                var text = StageContext.Current.Messages.GetText(_id_alt);
                StageContext.Current.Dialog.ShowMessage(text);
            }
            else
            {
                var text = StageContext.Current.Messages.GetText(_id);
                StageContext.Current.Dialog.ShowMessage(text);
            }

            await UniTask.Delay(500, cancellationToken: token);
        }

        public UniTask OnExitAsync(CancellationToken token = default)
        {
            StageContext.Current.Dialog.HideMessage();
            return UniTask.CompletedTask;
        }
    }
}
