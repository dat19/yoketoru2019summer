using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBehaviour : MonoBehaviour
{
    bool isStarted = false;

    void Update()
    {
        if (isStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            isStarted = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Game");
        }
    }
}
