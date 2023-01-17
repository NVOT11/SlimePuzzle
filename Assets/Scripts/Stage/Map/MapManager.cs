using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Stage
{
    public class MapManager
    {
        /// <summary>
        /// ある地点のトラバーサルを取得する
        /// </summary>
        public static Traversal CheckTraversal(Vector3 origin, Direction direction)
        {
            var frontPoint = origin + direction.ToVector();
            var results = Physics2D.OverlapAreaAll(frontPoint, frontPoint);

            var list = new List<ITraversable>();
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].gameObject.TryGetComponent<ITraversable>(out var checkable))
                {
                    list.Add(checkable);
                }
            }

            var top = list.OrderByDescending(_ => _.TravasalPriority).FirstOrDefault();
            return top?.Travase ?? Traversal.Undified;
        }

        /// <summary>
        /// ある地点のIInteractableを取得する
        /// 何もなければ空のオブジェクトを返す
        /// </summary>
        public static IInteractable CheckInteraction(Vector3 origin, Direction direction)
        {
            var frontPoint = origin + direction.ToVector();
            var results = Physics2D.OverlapAreaAll(frontPoint, frontPoint);

            var interactables = new List<IInteractable>();
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].gameObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactables.Add(interactable);
                }
            }

            return interactables.OrderByDescending(_ => _.InteractionPriority).FirstOrDefault()
                ?? new EmptyMapObject();
        }

        /// <summary>
        /// ある地点のスライムを取得する
        /// </summary>
        public static SlimeObject CheckSlime(Vector3 origin, Direction direction)
        {
            var frontPoint = origin + direction.ToVector();
            var results = Physics2D.OverlapAreaAll(frontPoint, frontPoint);

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].gameObject.TryGetComponent<SlimeObject>(out var slime))
                {
                    return slime;
                }
            }

            return null;
        }

        // 周囲4マスのトラバーサルを調べて
        // 進行可能なマスがあればTrueを返し、Directionを返す
        // なければFalseを返す
        public static bool CheckAround(Vector3 origin, out Direction direction)
        {
            direction = Direction.None;

            var up = CheckTraversal(origin, Direction.Up);
            if (up == Traversal.CanEnter)
            {
                direction = Direction.Up;
                return true;
            }

            var down = CheckTraversal(origin, Direction.Down);
            if (down == Traversal.CanEnter)
            {
                direction = Direction.Down;
                return true;
            }

            var left = CheckTraversal(origin, Direction.Left);
            if (left == Traversal.CanEnter)
            {
                direction = Direction.Left;
                return true;
            }

            var right = CheckTraversal(origin, Direction.Right);
            if (right == Traversal.CanEnter)
            {
                direction = Direction.Right;
                return true;
            }

            return false;
        }
    }
}
