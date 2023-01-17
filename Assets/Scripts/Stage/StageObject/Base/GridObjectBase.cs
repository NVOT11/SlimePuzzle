using UnityEngine;

namespace Stage
{
    /// <summary>
    /// エディタ用
    /// </summary>
    public class GridObjectBase : MonoBehaviour
    {
        public void Up()
        {
            this.transform.position += Vector3.up;
            Adjust();
        }

        public void Down()
        {
            this.transform.position += Vector3.down;
            Adjust();
        }

        public void Right()
        {
            this.transform.position += Vector3.right;
            Adjust();
        }

        public void Left()
        {
            this.transform.position += Vector3.left;
            Adjust();
        }

        public void Adjust()
        {
            var x = Mathf.RoundToInt(this.transform.position.x);
            var y = Mathf.RoundToInt(this.transform.position.y);
            var z = this.transform.position.z;

            this.transform.position = new Vector3(x, y, z);
        }
    }
}
