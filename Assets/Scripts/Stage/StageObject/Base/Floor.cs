using UnityEngine;

namespace Stage
{
    /// <summary>
    /// 床、基本的に通行可能
    /// 壁などが上にあれば通れない
    /// </summary>
    public class Floor : MonoBehaviour, ITraversable
    {
        public int TravasalPriority => TRAVASAL.FLOOR;
        public Traversal Travase => Traversal.CanEnter;
    }
}
