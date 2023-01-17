using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
using Common;

namespace Stage
{
    public class PlayerFollowCamera
    {
        private Camera _camera;
        private Transform _target;

        private bool _enabled = true;

        public PlayerFollowCamera(Camera camera)
        {
            _camera = camera;
        }

        public void Run(GameObject token)
        {
            token.LateUpdateAsObservable()
                .Where(_ => _target != null)
                .Where(_ => _enabled)
                .Subscribe(_ => ManagedUpdate())
                .AddTo(token);
        }

        private void ManagedUpdate()
        {
            var targetPositon = new Vector3(_target.position.x, _target.position.y, _camera.transform.position.z);
            _camera.transform.position = targetPositon;
        }

        public void SetTarget(Transform targetTransform)
        {
            _target = targetTransform;
            _enabled = true;
        }

        public async UniTaskVoid ChangeTarget(Transform targetTransform)
        {
            if (targetTransform == _target) return;
            _enabled = false;
            _target = targetTransform;

            var targetPositon = new Vector3(_target.position.x, _target.position.y, _camera.transform.position.z);
            await _camera.transform.DOMove(targetPositon, 0.2f).WithCancellation(Token);

            _enabled = true;
        }

        private CancellationToken Token => _camera.GetCancellationTokenOnDestroy();
    }
}