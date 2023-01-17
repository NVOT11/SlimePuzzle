using UnityEngine;

namespace Stage
{
    /// <summary>
    /// プレイヤーの初期位置
    /// 特に処理することはない
    /// </summary>
    public class SpawnPoint : GridObjectBase
    {
        private void Awake()
        {
            var renderer = this.GetComponent<Renderer>();
            if (renderer) renderer.enabled = false;
        }
    }
}
