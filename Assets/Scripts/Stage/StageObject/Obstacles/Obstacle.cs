namespace Stage
{
    /// <summary>
    /// 何もしない障害物
    /// </summary>
    public class Obstacle : GridObjectBase, ITraversable
    {
        public int TravasalPriority => TRAVASAL.OBSTACLE;
        public Traversal Travase => Traversal.Forbidden;

    }
}
