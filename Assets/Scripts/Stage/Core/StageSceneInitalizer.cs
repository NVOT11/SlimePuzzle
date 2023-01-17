using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Threading;
using UnityEngine;
using UniRx;
using Common;
using Common.StaticData;

namespace Stage
{
    public class StageSceneInitalizer : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Dialogbox _dialogbox;
        [SerializeField] private HUD _HUD;
        [SerializeField] private VirtualPad _virtualPad;
        [SerializeField] private EndPanel _endPanel;

        private void Awake()
        {
            var token = this.GetCancellationTokenOnDestroy();
            InitializeAsync(token).Forget();
        }

        private async UniTask InitializeAsync(CancellationToken token)
        {
            // コンテキスト生成 シーンに紐づけ
            var context = new StageContext();
            context.AddTo(this);

            // UniTaskやUnirxに使うトークン
            context.SceneToken = this.gameObject;

            // 入力を切っておく
            context.InputEnabled = false;

            context.FollowCamera = new PlayerFollowCamera(_camera);

            // Prefabをセット
            context.Maps = GameContext.Current.Setting.MapObjects;
            context.Prototype = GameContext.Current.Setting.ProtoType;

            // 
            context.LibrariyList = new LibrariyList(GameContext.Current.Setting.Libiraries);

            // UI
            context.VPad = _virtualPad;
            context.HUD = _HUD;
            context.Dialog = _dialogbox;
            context.EndPanel = _endPanel;

            //
            var textAsset = GameContext.Current.Setting.DialogText;
            var loaded = DialogDataResolver.GetTexts(textAsset);
            context.Messages = new DialogTextData(loaded);

            //
            var stageName = GameContext.Current.GetStageName(GameContext.Current.CurrentRank);
            context.HUD.SetFloorName(stageName);

            // Startまで待機
            await this.StartAsync();

            // コントローラー起動
            context.Controller.Run(this.gameObject);

            // 開始
            context.StateMachine.SetState(new InitialState());
        }
    }
}