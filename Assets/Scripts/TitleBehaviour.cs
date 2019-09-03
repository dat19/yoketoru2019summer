using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBehaviour : MonoBehaviour
{
    bool isStarted = false;

    private void Start()
    {
        SoundManager.PlayBGM(SoundManager.AUDIO_LIST.TITLE);
    }

    void Update()
    {
        if (isStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            isStarted = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Game");
            SoundManager.StopBGM();
            SoundManager.PlaySE(SoundManager.AUDIO_LIST.START);            
        }
    }
}
