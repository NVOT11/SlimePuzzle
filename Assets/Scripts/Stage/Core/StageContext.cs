using UnityEngine;
using Common;
using System;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using UniRx;

namespace Stage
{
    public class StageContext : IDisposable
    {
        public static StageContext Current { get; set; }
        public StageContext() => Current = this;

        public StateMachine StateMachine { get; set; } = new StateMachine();

        //
        public Slime Slime => SlimeList.CurrentSlime;
        public SlimeList SlimeList { get; set; } = new SlimeList();

        public ReactiveProperty<SlimeMode> RxSlimeMode { get; set; }
            = new ReactiveProperty<SlimeMode>(SlimeMode.Green);

        //
        public SlimeObject Prototype { get; set; }
        public MapObject[] Maps { get; set; }

        //
        public int CurrentRank => GameContext.Current.CurrentRank;

        //
        public SlimeController Controller { get; set; } = new SlimeController();
        public bool InputEnabled { get; set; } = false;

        //
        public GameObject SceneToken { get; set; }
        //
        public PlayerFollowCamera FollowCamera { get; set; }

        // UI
        public VirtualPad VPad { get; internal set; }
        public HUD HUD { get; internal set; }
        public Dialogbox Dialog { get; internal set; }
        public EndPanel EndPanel { get; internal set; }

        // 
        public DialogTextData Messages { get; internal set; }

        //
        public SpriteLibraryAsset[] Library { get; internal set; }
        public LibrariyList LibrariyList { get; set; }

        /// <summary>
        /// 最終ステージ終わりにEDフラグが立つ
        /// </summary>
        public bool IsEnding()
        {
            if (GameContext.Current.Debug.WatchED) return true;
            return GameContext.Current.EDFlag;
        }

        public void Dispose()
        {
            StateMachine.Dispose();
        }

        // FastMode
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            Current = null;
        }
    }

}