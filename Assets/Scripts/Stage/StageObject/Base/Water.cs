using UnityEngine;

namespace Stage
{
    /// <summary>
    /// 壁、通行不可になる
    /// </summary>
    public class Water : MonoBehaviour, ITraversable
    {
        public int TravasalPriority => TRAVASAL.WATER;

        public Traversal Travase => IsBlue() ? Traversal.CanEnter : Traversal.Forbidden;

        public bool IsBlue() => StageContext.Current.Slime.Mode == SlimeMode.Blue;
    }
}
