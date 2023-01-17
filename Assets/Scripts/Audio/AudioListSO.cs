using UnityEngine;

namespace Audio
{
    /// <summary>
    /// クリップ登録用
    /// </summary>
    [CreateAssetMenu(menuName = nameof(AudioListSO))]
    public class AudioListSO : ScriptableObject
    {
        public AudioClip[] BGMs => _BGMs;
        [SerializeField] private AudioClip[] _BGMs;

        public AudioClip[] Sounds => _sounds;
        [SerializeField] private AudioClip[] _sounds;
    }
}