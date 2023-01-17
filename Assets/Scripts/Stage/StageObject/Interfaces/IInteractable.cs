using Cysharp.Threading.Tasks;
using System.Threading;

namespace Stage
{
    /// <summary>
    /// �����ł���I�u�W�F�N�g�Ɏ�������
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// �D��x�̍������̂��Q�Ƃ���
        /// </summary>
        int InteractionPriority { get; }

        /// <summary>
        /// ���s���e
        /// </summary>
        public UniTask OnInteractAsync(CancellationToken token = default);

        /// <summary>
        /// ���ꂽ��
        /// �������Ȃ����Ƃ̕�������
        /// </summary>
        public UniTask OnExitAsync(CancellationToken token = default) => UniTask.CompletedTask;
    }
}
