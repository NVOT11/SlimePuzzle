namespace Stage
{
    public static class TRAVASAL
    {
        /// <summary>
        /// スライムが上に乗っている場合がある
        /// </summary>
        public static int SLIME => 5;
        /// <summary>
        /// 床に乗っている物
        /// </summary>
        public static int OBSTACLE => 3;
        /// <summary>
        /// 移動床など特殊な床
        /// </summary>
        public static int OVERFLOOR => 2;
        /// <summary>
        /// 床よりは優先
        /// </summary>
        public static int WATER => 1;
        /// <summary>
        /// 壁より上
        /// </summary>
        public static int FLOOR => 0;
        /// <summary>
        /// 絶対に進行不可
        /// </summary>
        public static int WALL => -1;
    }
}
