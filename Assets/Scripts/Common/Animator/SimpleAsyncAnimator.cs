using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UniRx;
using System.Collections.Generic;

namespace Common.Animation
{
    /// <summary>
    /// SpriteLibraryを使った
    /// 簡易的なアニメーション再生クラス
    /// </summary>
    public class SimpleAsyncAnimator : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected SpriteLibraryAsset _library;
        [SerializeField] protected string _initialLabel;
        [SerializeField] protected int _interval = 100;
        [SerializeField] protected bool _loop = true;
        [SerializeField] protected bool _playOnAwake = true;

        protected List<Sprite> _sprites = new List<Sprite>(10);

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public virtual void Awake()
        {
            _cts?.AddTo(this);

            if (_playOnAwake)
            {
                SetSprites(_initialLabel);
                PlayAsync(_loop).Forget();
            }
        }

        public void Cancel(bool instantNew = true)
        {
            _cts?.Cancel();

            if (instantNew)
            {
                _cts = new CancellationTokenSource();
                _cts.AddTo(this);
            }
        }

        /// <summary>
        /// ライブラリーを変更する
        /// </summary>
        public virtual void SetLibrary(SpriteLibraryAsset spriteLibrary)
        {
            _library = spriteLibrary;
        }

        /// <summary>
        /// 所持ライブラリーからSpriteをセットする
        /// </summary>
        public virtual void SetSprites(string label)
        {
            var entryNames = _library.GetCategoryLabelNames(label);
            _sprites.Clear();
            foreach (var entryName in entryNames)
            {
                var sprite = _library.GetSprite(label, entryName);
                _sprites.Add(sprite);
            }
        }

        /// <summary>
        /// Spriteリストを順に表示する
        /// </summary>
        public virtual async UniTask PlayAsync(bool loop = true)
        {
            Cancel();
            bool nextLoop = true;
            while (!_cts.Token.IsCancellationRequested && nextLoop)
            {
                _cts.Token.ThrowIfCancellationRequested();

                foreach (var sprite in _sprites)
                {
                    if (!sprite) return;
                    if (!_spriteRenderer) return;

                    _spriteRenderer.sprite = sprite;
                    await UniTask.Delay(_interval, cancellationToken: _cts.Token);
                }

                nextLoop = loop ? true : false;

            }
        }
    }
}