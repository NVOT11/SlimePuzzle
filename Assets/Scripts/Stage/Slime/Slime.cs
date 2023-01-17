using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UniRx;
using Audio;

namespace Stage
{
    public class Slime
    {
        /// <summary>
        /// インスタンス時にシーン上のキャラと紐付ける
        /// </summary>
        /// <param name="onScene"></param>
        public Slime(SlimeObject onScene)
        {
            _onScene = onScene;
            _onScene.VirtualSlime = this;

            _cts.AddTo(_onScene);
        }

        /// <summary>
        /// 実体としてのシーン上のオブジェクト
        /// </summary>
        public SlimeObject OnScene => _onScene;
        private SlimeObject _onScene;

        /// <summary>
        /// シーン上のTransform
        /// </summary>
        public Transform Transform => _onScene.transform;
        public Vector3 Position => Transform.position;

        /// <summary>
        /// 操作状態
        /// </summary>
        public SlimeState SlimeState { get; private set; } = SlimeState.Idle;
        /// Shortcut
        private bool CanAction => SlimeState == SlimeState.Idle;

        /// <summary>
        /// 緑、赤のモード
        /// </summary>
        public SlimeMode Mode { get; internal set; }

        /// <summary>
        /// 進行方向
        /// </summary>
        public Direction CurrentDirection { get; private set; } = Direction.Up;

        public void SetLock(bool locking)
        {
            SlimeState = locking ? SlimeState.Locked : SlimeState.Idle;
        }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// コントローラーからの操作は同期
        /// </summary>
        internal void Move()
        {
            MoveAsync(_cts.Token).Forget();
        }

        internal void Duplicate()
        {
            DuplicateAsync(_cts.Token).Forget();
        }

        /// <summary>
        /// 生成時処理
        /// </summary>
        public async UniTask ActivateAsync(CancellationToken token = default)
        {
            await CheckBelow().OnInteractAsync(token);
            _onScene.OnModeChange(Mode);
        }

        private void OnDead()
        {
            AudioManager.Current.PlaySE("Union");
            StageContext.Current.SlimeList.RemoveSlime(this);
            _onScene.OnUnion();
        }

        internal void ModeChange(SlimeMode mode)
        {
            Mode = mode;
            _onScene.OnModeChange(mode);
        }

        // 移動関係

        /// <summary>
        /// 進行方向をセットする
        /// </summary>
        /// <param name="direction"></param>
        public void SetDirection(Direction direction)
        {
            CurrentDirection = direction;
        }

        /// <summary>
        /// 進行方向へ移動を実行する
        /// </summary>
        public async UniTask MoveAsync(CancellationToken token = default)
        {
            // 移動可能状態か
            if (!CanAction) return;

            var slime = CheckSlime();
            if (slime) slime.VirtualSlime.OnDead();

            // 進行方向は通行可能か
            if (CheckFrontObject() != Traversal.CanEnter) return;

            // 移動状態にする
            SlimeState = SlimeState.Moving;

            // 足元オブジェクトの離脱処理
            CheckBelow()?.OnExitAsync(token);

            // 移動処理
            var offset = CurrentDirection.ToVector();
            int count = 0;
            while (!token.IsCancellationRequested && count < 10)
            {
                Transform.position += offset * 0.1f;
                await UniTask.Delay(20, cancellationToken: token);
                count++;
            }

            // 足元を調べる
            var below = CheckBelow();
            await below.OnInteractAsync(token);

            // 完了
            SlimeState = SlimeState.Idle;
        }

        /// <summary>
        /// 進行方向のトラバーサルを取得する
        /// </summary>
        public Traversal CheckFrontObject()
        {
            return MapManager.CheckTraversal(Position, CurrentDirection);
        }

        /// <summary>
        /// 足元のInteractableを取得する
        /// </summary>
        public IInteractable CheckBelow()
        {
            return MapManager.CheckInteraction(Position, Direction.None);
        }

        /// <summary>
        /// 進行方向のスライムを取得する
        /// </summary>        
        public SlimeObject CheckSlime()
        {
            return MapManager.CheckSlime(Position, CurrentDirection);
        }

        /// <summary>
        /// 自身のコピーを生成する
        /// </summary>
        public async UniTask DuplicateAsync(CancellationToken token = default)
        {
            if (!CanAction) return;

            var limited = StageContext.Current.SlimeList.IsLimit();
            if (limited)
            {
                AudioManager.Current.PlaySE("Miss");
                return;
            }

            SlimeState = SlimeState.InAction;

            if (CheckFrontObject() == Traversal.CanEnter)
            {
                var duplicated = SlimeGanerator.DupricateAsync(this);
                StageContext.Current.SlimeList.ChangeSlime(duplicated);
                AudioManager.Current.PlaySE("Divide");
                await duplicated.ActivateAsync(token);
            }
            else if (MapManager.CheckAround(Position, out var direction))
            {
                var duplicated = SlimeGanerator.DupricateAsync(this, direction);
                StageContext.Current.SlimeList.ChangeSlime(duplicated);
                AudioManager.Current.PlaySE("Divide");
                await duplicated.ActivateAsync(token);
            }
            else
            {
                AudioManager.Current.PlaySE("Miss");
            }

            SlimeState = SlimeState.Idle;
        }

        /// <summary>
        /// 操作状態になった
        /// </summary>
        public void OnSelect(bool select = true, bool sound = false)
        {
            if (sound) AudioManager.Current.PlaySE("Select");
            if (select) _onScene.Selected().Forget();
        }

        /// <summary>
        /// 操作状態でなくなった
        /// </summary>
        public void OnDeselect()
        {
            _onScene.Deselected();
        }
    }
}
