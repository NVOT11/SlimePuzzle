using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;

namespace Common
{
    public class GameInit
    {
        private const string InitializeSceneName = "Initialize";
        private const string TitleSceneName = "Title";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnBeforeLoadFirstScene()
        {
            Debug.Log("-Init-");

            // ここで一回のみインスタンス
            new GameContext();

            // データロード
            GameContext.Current.SettingData.Load();
            GameContext.Current.SaveData.Load();

            //
            new AudioManager();

            // 初期化シーンをロード
            if (!SceneManager.GetSceneByName(InitializeSceneName).IsValid())
            {
                SceneManager.LoadScene(InitializeSceneName, LoadSceneMode.Additive);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void OnAfterLoadFirstScene()
        {
            // エディタならスルー
            if (Application.isEditor) return;

            // ビルド時のみタイトルへ
            if (!SceneManager.GetSceneByName(TitleSceneName).IsValid())
            {
                SceneManager.LoadScene(TitleSceneName, LoadSceneMode.Single);
            }
        }
    }
}