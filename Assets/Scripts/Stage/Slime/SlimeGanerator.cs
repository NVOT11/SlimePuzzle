using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Stage
{
    public class SlimeGanerator
    {
        public static Slime DupricateAsync(Slime origin)
        {
            // 進行方向
            var target = origin.Position + origin.CurrentDirection.ToVector();

            // 生成
            var proto = StageContext.Current.Prototype.Create(target, origin.Mode);
            var dupricated = new Slime(proto);

            // 初期設定
            dupricated.Mode = origin.Mode;
            dupricated.SetDirection(origin.CurrentDirection);

            // リストに追加して交替
            StageContext.Current.SlimeList.AddSlime(dupricated);

            return dupricated;
        }

        /// <summary>
        /// 方向指定あり
        /// </summary>
        public static Slime DupricateAsync(Slime origin, Direction direction)
        {
            // 進行方向
            var target = origin.Position + direction.ToVector();

            // 生成
            var proto = StageContext.Current.Prototype.Create(target, origin.Mode);
            var dupricated = new Slime(proto);

            // 初期設定
            dupricated.Mode = origin.Mode;
            dupricated.SetDirection(origin.CurrentDirection);

            // リストに追加して交替
            StageContext.Current.SlimeList.AddSlime(dupricated);

            return dupricated;
        }

        public static Slime InitialGenarate(Vector3 initialPoint)
        {
            var proto = StageContext.Current.Prototype.Create(initialPoint);
            var slime = new Slime(proto);
            StageContext.Current.SlimeList.AddSlime(slime);

            slime.OnSelect(false);

            return slime;
        }
    }
}
