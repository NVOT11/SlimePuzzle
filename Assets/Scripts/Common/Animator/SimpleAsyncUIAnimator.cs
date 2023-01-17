using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Common.Animation
{
    public class SimpleAsyncUIAnimator : MonoBehaviour
    {
        [SerializeField] protected Image _spriteRenderer;
        [SerializeField] protected SpriteLibraryAsset _library;
        [SerializeField] protected string _initialLabel;
        [SerializeField] protected int _interval = 100;
        [SerializeField] protected bool _loop = true;
        [SerializeField] protected bool _playOnAwake = true;

        protected List<Sprite> _sprites = new List<Sprite>(10);

        public virtual void Awake()
        {
            if (_playOnAwake)
            {
                SetSprites(_initialLabel);
                var token = this.GetCancellationTokenOnDestroy();
                PlayAsync(_loop, token).Forget();
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
        public virtual async UniTask PlayAsync(bool loop = true, CancellationToken token = default)
        {
            bool nextLoop = true;
            while (!token.IsCancellationRequested && nextLoop)
            {
                foreach (var sprite in _sprites)
                {
                    if (!sprite) return;
                    if (!_spriteRenderer) return;

                    _spriteRenderer.sprite = sprite;
                    await UniTask.Delay(_interval, cancellationToken: token);
                }

                nextLoop = loop ? true : false;
            }
        }
    }
}