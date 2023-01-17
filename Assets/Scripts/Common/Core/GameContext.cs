using UnityEngine;
using UniRx;
using Common.StaticData;
using System.Linq;
using Common.Save;

namespace Common
{
    public class GameContext
    {
        public static GameContext Current { get; private set; }

        public GameContext()
        {
            Current = this;

            _token = new GameObject("TOKEN");
            GameObject.DontDestroyOnLoad(_token);

            InputManager.Run(_token);

            StageRank.SetRank(Debug.InitialRank);

            StageInfo = StageInfoResolver.GetTexts(Setting.StageInfo);
        }

        public SaveData SaveData { get; set; } = new SaveData();

        public SettingData SettingData { get; set; } = new SettingData();

        public StageRank StageRank { get; set; } = new StageRank();
        public int CurrentRank => StageRank.Rank;

        public StageInfo[] StageInfo { get; set; }
        public string GetStageName(int rank) => StageInfo.FirstOrDefault(_ => _.Rank == rank).Name;

        public DebugSO Debug => _debugSO ??= Resources.Load<DebugSO>(nameof(DebugSO));
        private DebugSO _debugSO = null;

        public GameSetting Setting => _setting ??= Resources.Load<GameSetting>(nameof(GameSetting));
        private GameSetting _setting = null;

        public GameObject PersitantToken => _token;

        public bool EDFlag { get; internal set; }

        private GameObject _token;

    }
}
