using UnityEngine;

namespace Common
{
    public class StageRank
    {
        public int Rank { get; private set; } = 1;

        private int Initial = 1;
        private int Max => GameContext.Current.Setting.MapObjects.Length;

        public void SetRank(int rank)
        {
            var result = Mathf.Clamp(rank, Initial, Max);
            Rank = result;
        }

        public void NextRank()
        {
            SetRank(Rank + 1);
        }

        public bool IsFinalStageFinished => Rank >= Max;
    }
}
