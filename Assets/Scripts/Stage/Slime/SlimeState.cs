namespace Stage
{
    public enum SlimeState 
    {
        /// <summary>
        /// 操作可能
        /// </summary>
        Idle,
        /// <summary>
        /// 移動中
        /// </summary>
        Moving, 
        /// <summary>
        /// 何らかのアクション
        /// </summary>
        InAction, 
        /// <summary>
        /// ギミックによって操作されている
        /// </summary>
        Locked,
    }
}
