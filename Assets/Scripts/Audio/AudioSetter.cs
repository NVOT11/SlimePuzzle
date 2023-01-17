using UnityEngine;

namespace Audio
{
    /// <summary>
    /// 初期設定
    /// </summary>
    public class AudioSetter : MonoBehaviour
    {
        [SerializeField] private AudioListSO AudioListSO;
        [SerializeField] private AudioSource BGMPlayer;
        [SerializeField] private AudioSource[] SEPlayers;

        private void Awake()
        {
            // クリップリストを渡す
            AudioManager.Current.BGMs = AudioListSO.BGMs;
            AudioManager.Current.Sounds = AudioListSO.Sounds;
     
            // AudioSourceを渡す
            AudioManager.Current.BGMPlayer = BGMPlayer;
            AudioManager.Current.SEChanels = SEPlayers;

            AudioManager.Current.VolumeController.SetActualSourceVolume();
        }
    }
}