using UnityEngine;

namespace Audio
{
    public class VolumeController
    {
        private AudioManager _audioManager;

        public VolumeController(AudioManager manager)
        {
            _audioManager = manager;
        }

        /// <summary>
        /// 実際のオーディオソースに反映する
        /// これいる？
        /// </summary>
        public void SetActualSourceVolume()
        {
            // 実際に反映
            _audioManager.BGMPlayer.volume = BGMVolume;

            foreach (var source in _audioManager.SEChanels)
            {
                source.volume = SEVolume;
            }
        }

        // BGM
        public float BGMVolume { get; private set; } = 0.8f;
        public float MaxBGM { get; set; } = 1.0f;
        public float MinBGM { get; set; } = 0.0f;

        /// <summary>
        /// あくまで仮想のボリューム
        /// </summary>
        public void SetBGMVolume(float value)
        {
            var result = Mathf.Clamp(value, MinBGM, MaxBGM);
            BGMVolume = result;
        }

        // SE
        public float SEVolume { get; private set; } = 0.8f;
        public float MaxSE { get; set; } = 1.0f;
        public float MinSE { get; set; } = 0.0f;

        /// <summary>
        /// あくまで仮想のボリューム
        /// </summary>
        public void SetSEVolume(float value)
        {
            var result = Mathf.Clamp(value, MinSE, MaxSE);
            SEVolume = result;
        }
    }
}