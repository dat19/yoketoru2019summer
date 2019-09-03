using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBehaviour : MonoBehaviour
{
    bool isStarted = false;
    bool isRanking = false;

    private void Start()
    {
        SoundManager.PlayBGM(SoundManager.AUDIO_LIST.TITLE);
        if (GameParams.isHighScore)
        {
            ShowRanking();
        }
    }

    private void FixedUpdate()
    {
        if (isRanking)
        {
            if (!SceneManager.GetSceneByName("Ranking").IsValid())
            {
                SoundManager.PlaySE(SoundManager.AUDIO_LIST.CLICK);
                isRanking = false;
            }
        }
    }

    public void GameStart()
    {
        if (isStarted || isRanking) return;

        isStarted = true;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Game");
        SoundManager.StopBGM();
        SoundManager.PlaySE(SoundManager.AUDIO_LIST.START);
    }

    public void ShowRanking()
    {
        if (isRanking) return;

        SoundManager.PlaySE(SoundManager.AUDIO_LIST.CLICK);
        isRanking = true;
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(GameParams.highScore);
    }
}
