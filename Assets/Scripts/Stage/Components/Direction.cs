using UnityEngine;

namespace Stage
{
    public enum Direction { None, Up, Down, Right, Left, }

    public static class Util
    {
        public static Vector3 ToVector(this Direction direction)
        {
            return direction switch
            {
                Direction.None => Vector3.zero,
                Direction.Up => Vector3.up,
                Direction.Down => Vector3.down,
                Direction.Right => Vector3.right,
                Direction.Left => Vector3.left,
                _ => Vector3.zero,
            };
        }

        public static Direction ToDirection(this Vector2 vector2)
        {
            if (vector2.y > 0) return Direction.Up;
            if (vector2.y < 0) return Direction.Down;
            if (vector2.x > 0) return Direction.Right;
            if (vector2.x < 0) return Direction.Left;

            return Direction.None;
        }
    }
}
