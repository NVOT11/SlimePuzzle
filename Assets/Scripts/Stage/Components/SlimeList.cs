using System;
using System.Collections.Generic;
using UniRx;

namespace Stage
{
    public class SlimeList
    {
        public List<Slime> Slimes => _slimes;
        private List<Slime> _slimes = new List<Slime>(0);

        public Slime CurrentSlime => _slimes[_index];
        private int _index = 0;

        public Subject<List<Slime>> OnListChanged { get; set; } = new Subject<List<Slime>>();

        /// <summary>
        /// 最大数
        /// </summary>
        public int MaxSlime => 3;

        /// <summary>
        /// 最大数に達しているか
        /// </summary>
        public bool IsLimit() => _slimes.Count >= MaxSlime;

        /// <summary>
        /// 追加する
        /// </summary>
        public void AddSlime(Slime player)
        {
            _slimes.Add(player);

            OnListChanged.OnNext(_slimes);
        }

        /// <summary>
        /// 取り除く
        /// </summary>
        public void RemoveSlime(Slime target)
        {
            var current = StageContext.Current.Slime;

            if (target == current)
            {
                // 削除対象が操作中ならインデックスを回してから削除
                NextSlime();
                _slimes.Remove(target);
            }
            else
            {
                // 削除対象が操作中でないなら、削除後にインデックスを修正
                _slimes.Remove(target);
                _index = _slimes.IndexOf(current);
            }

            OnListChanged.OnNext(_slimes);
        }

        /// <summary>
        /// 操作スライムを次に回す
        /// </summary>
        public void NextSlime()
        {
            _index = (_index + 1) % _slimes.Count;

            foreach (var slime in _slimes)
            {
                if (slime == CurrentSlime)
                {
                    slime.OnSelect(sound:true);
                    StageContext.Current.FollowCamera.ChangeTarget(slime.Transform);
                }
                else slime.OnDeselect();
            }
        }

        /// <summary>
        /// 操作スライムを指定して切り替える
        /// </summary>
        public void ChangeSlime(Slime target)
        {
            _index = _slimes.IndexOf(target);

            foreach (var slime in _slimes)
            {
                if (slime == CurrentSlime)
                {
                    slime.OnSelect();
                    StageContext.Current.FollowCamera.ChangeTarget(slime.Transform);
                }
                else slime.OnDeselect();
            }
        }
    }
}