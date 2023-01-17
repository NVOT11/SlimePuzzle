using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = nameof(DebugSO), menuName = nameof(DebugSO))]
    public class DebugSO : ScriptableObject
    {
        public int InitialRank => _initialRank;
        [SerializeField] private int _initialRank;

        public bool WatchED => _watchED;
        [SerializeField] private bool _watchED;

        public bool Check => _initialCheck;
        [SerializeField] private bool _initialCheck;
    }
}