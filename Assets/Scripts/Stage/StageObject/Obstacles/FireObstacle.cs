namespace Stage
{
    /// <summary>
    /// Redなら通行可能になる
    /// </summary>
    public class FireObstacle : GridObjectBase, ITraversable
    {
        public int TravasalPriority => TRAVASAL.OBSTACLE;
        public Traversal Travase => IsRed() ? Traversal.CanEnter : Traversal.Forbidden;

        public bool IsRed() => StageContext.Current.Slime.Mode == SlimeMode.Red;
    }
}
