﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToTitle : MonoBehaviour
{
    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title");
        }        
    }
}
