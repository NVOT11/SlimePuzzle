namespace Stage
{
    /// <summary>
    /// 通行に関わるオブジェクトに実装する
    /// </summary>
    public interface ITraversable
    {
        /// <summary>
        /// 最も優先度が高いものを参照する
        /// </summary>
        public int TravasalPriority { get; }

        /// <summary>
        /// 通行可否
        /// </summary>
        public Traversal Travase { get; }
    }
}
