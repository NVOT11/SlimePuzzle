using UnityEngine;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Audio
{
    /// <summary>
    /// 再生機能
    /// </summary>
    public class AudioManager
    {
        public static AudioManager Current { get; set; }

        public AudioManager()
        {
            Current = this;
            VolumeController = new VolumeController(this);

            _SoundQueue = new Queue<AudioClip>(10);
        }

        public VolumeController VolumeController { get; private set; }

        internal void Clear()
        {
            _SoundQueue.Clear();
        }

        // SceneComponent
        public AudioSource BGMPlayer { get; internal set; }
        public AudioSource[] SEChanels { get; internal set; }

        // ClupList
        public AudioClip[] BGMs { get; set; }
        public AudioClip[] Sounds { get; set; }

        // BGM

        public string PlayingBGMName => BGMPlayer.clip==null ? "" : BGMPlayer.clip.name;

        /// <summary>
        /// Clip名で指定
        /// </summary>
        public void PlayBGM(string soundName = "")
        {
            if (string.IsNullOrWhiteSpace(soundName)) return;

            var clip = BGMs.FirstOrDefault(_ => _.name == soundName);
            if (clip != null)
            {
                // 見つかれば再生メソッドへ
                this.PlayBGM(clip);
            }
        }

        // 再生時は必ずこれを呼ぶ
        public void PlayBGM(AudioClip clip)
        {
            if (BGMPlayer == null || clip == null) return;

            BGMPlayer.clip = clip;
            BGMPlayer.Play();
        }

        public void PauseBGM()
        {
            BGMPlayer?.Pause();
        }

        public void UnPauseBGM()
        {
            BGMPlayer?.Play();
        }

        // SE

        // 使用チャネル
        private int currentChannelIndex = 0;
        private bool isRunning = false;
        Queue<AudioClip> _SoundQueue = new Queue<AudioClip>(10);
        private int IntervalMilSec = 30;
        private CancellationTokenSource _CTS = new CancellationTokenSource();

        /// <summary>
        /// 外から呼ぶメソッド　名前で指定
        /// </summary>
        public void PlaySE(string soundName)
        {
            if (string.IsNullOrWhiteSpace(soundName)) return;

            var clip = Sounds.FirstOrDefault(_ => _.name == soundName);
            if (clip != null)
            {
                this.PlaySE(clip);
            }
        }

        /// <summary>
        /// 実際に再生するメソッド
        /// </summary>
        public void PlaySE(AudioClip clip)
        {
            if (SEChanels == null || clip == null) return;

            _SoundQueue.Enqueue(clip);

            if (!isRunning) RunPlayer().Forget();
        }

        public async UniTask RunPlayer()
        {
            var token = _CTS.Token;
            token.ThrowIfCancellationRequested();

            while (!token.IsCancellationRequested)
            {
                isRunning = true;
                if (_SoundQueue.TryDequeue(out var nextClip))
                {
                    GetNextChannel().PlayOneShot(nextClip);
                }

                // 同時再生を防ぐため
                await UniTask.Delay(IntervalMilSec, cancellationToken: token);
            }

            // 抜けたらオフ
            isRunning = false;
        }

        private AudioSource GetNextChannel()
        {
            // 最後尾ならゼロに戻り、そうでなければインクリメントする
            var atLastChannel = currentChannelIndex >= SEChanels.Length - 1;
            currentChannelIndex = atLastChannel ? 0 : currentChannelIndex++;

            return SEChanels[currentChannelIndex];
        }

    }
}