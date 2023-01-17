using UnityEngine;

namespace Stage
{
    /// <summary>
    /// 壁、通行不可になる
    /// </summary>
    public class Wall : MonoBehaviour, ITraversable
    {
        public int TravasalPriority => TRAVASAL.WALL;
        public Traversal Travase => Traversal.Forbidden;
    }
}
