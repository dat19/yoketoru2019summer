using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    /// <summary>
    /// サウンドリスト
    /// </summary>
    public enum AUDIO_LIST
    {
        TITLE,
        BGM,
        GAMEOVER,
        START,
        CLICK,
        BOUND,
        ITEM,
        MISS,
        COUNTDOWN,
        GAMESTART
    }

    [Tooltip("オーディオクリップ"), SerializeField]
    AudioClip[] audioList = null;

    AudioSource _audioSource = null;

    private void Awake()
    {
        // 多重起動の時、破棄
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 指定の音をBGMとして鳴らします。
    /// </summary>
    /// <param name="bgm">BGM</param>
    public static void PlayBGM(AUDIO_LIST bgm)
    {
        Instance._playBGM(bgm);
    }

    /// <summary>
    /// 指定の音を効果音で鳴らします。
    /// </summary>
    /// <param name="se">鳴らしたい効果音</param>
    public static void PlaySE(AUDIO_LIST se)
    {
        Instance._playSE(se);
    }

    /// <summary>
    /// BGMを停止
    /// </summary>
    public static void StopBGM()
    {
        Instance._audioSource.Stop();
    }

    #region Private Methods
    void _playBGM(AUDIO_LIST bgm)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = audioList[(int)bgm];
        _audioSource.Play();
    }

    void _playSE(AUDIO_LIST se)
    {
        _audioSource.PlayOneShot(audioList[(int)se]);
    }

    #endregion Private Methods

}
