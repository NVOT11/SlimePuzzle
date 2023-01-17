using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniRx;
using System;
using Common;
using Common.Animation;

namespace Stage
{
    public class SlimeObject : MonoBehaviour, ITraversable
    {
        public SlimeObject Create(Vector3 postion, SlimeMode mode = SlimeMode.Green)
        {
            var slime = Instantiate(this, postion, Quaternion.identity);

            Cancel();

            _marker.SetActive(false);

            return slime;
        }

        // Property

        /// <summary>
        /// Animator
        /// </summary>
        public SimpleAsyncAnimator AsyncAnimator => _asyncAnimator;
        [SerializeField] private SimpleAsyncAnimator _asyncAnimator;

        /// <summary>
        /// Renderer
        /// </summary>
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Marker
        /// </summary>
        [SerializeField] private GameObject _marker;

        /// <summary>
        /// 仮想のクラス
        /// MapObjectから辿る用
        /// </summary>
        public Slime VirtualSlime { get; set; }

        /// <summary>
        /// Traversal
        /// </summary>
        public int TravasalPriority => TRAVASAL.SLIME;
        public Traversal Travase => _traversal;
        private Traversal _traversal = Traversal.Forbidden;

        // Mode
        public void OnModeChange(SlimeMode mode)
        {
            var library = StageContext.Current.LibrariyList.GetSpriteLibrary(mode);
            _asyncAnimator.SetLibrary(library);
            Idling();
        }

        public void Idling(CancellationToken token = default)
        {
            _asyncAnimator.SetSprites("Idle");
            _asyncAnimator.PlayAsync(true).Forget();
        }

        public async UniTask OnUnion()
        {
            GetComponent<Collider2D>().enabled = false;
            _traversal = Traversal.CanEnter;

            _asyncAnimator.SetSprites("Union");
            await _asyncAnimator.PlayAsync(false);

            Destroy(this.gameObject);
        }

        public async UniTask Selected()
        {
            Cancel();
            _marker.SetActive(true);
            await UniTask.Delay(1000, cancellationToken: _CTS.Token);
            _marker.SetActive(false);
        }

        public void Deselected()
        {
            _marker.SetActive(false);
        }

        private CancellationTokenSource _CTS = new CancellationTokenSource();
        public void Cancel()
        {
            _CTS?.Cancel();
            _CTS = new CancellationTokenSource();
            _CTS.AddTo(this.gameObject);
        }
    }
}
