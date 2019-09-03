using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour Instance;

    [Tooltip("スコアテキスト"), SerializeField]
    TextMeshProUGUI scoreText = null;
    [Tooltip("カウントダウンアニメーター"), SerializeField]
    Animator countDownAnimator = null;
    [Tooltip("カウントダウンテキスト"), SerializeField]
    TextMeshProUGUI countDownText = null;

    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GAME_STATE
    {
        READY,
        GAME,
        GAMEOVER,
        CLEAR
    }

    /// <summary>
    /// 現在のゲームの状態
    /// </summary>
    public static GAME_STATE state { get; private set; }

    /// <summary>
    /// 操作可能な状態の時、trueを返します。
    /// </summary>
    public static bool isPlayable
    {
        get
        {
            return state == GAME_STATE.GAME;
        }
    }

    #region Private Variables

    static int _countDown = 3;

    #endregion Private Variables


    private void Awake()
    {
        Instance = this;
        state = GAME_STATE.READY;
        Time.timeScale = 0f;
    }

    void Start()
    {
        GameParams.SetChangeScore(onChangeScore);
        GameParams.ClearScore();
        countDownAnimator.transform.root.gameObject.SetActive(true);
        _countDown = 3;
        countDownText.text = _countDown.ToString();
        countDownAnimator.SetTrigger("CountDown");
    }

    private void FixedUpdate()
    {
        if (!isPlayable) return;

        GameParams.AddScore((float)EnemySpawner.enemyCount*Time.fixedDeltaTime);
    }

    /// <summary>
    /// カウントダウンアニメーションから呼び出します。
    /// </summary>
    public static void CountDown()
    {
        if (!Instance.isActiveAndEnabled) return;

        _countDown--;
        if (_countDown >= 1)
        {
            Instance.countDownText.text = _countDown.ToString();
            Instance.countDownAnimator.SetTrigger("CountDown");
        }
        else if (_countDown == 0)
        {
            GameStart();
        }
        else
        {
            Instance.countDownAnimator.gameObject.transform.root.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// カウントダウンが完了して、ゲームを開始する時に呼び出します。
    /// </summary>
    static void GameStart()
    {
        Instance.countDownText.text = "START!!";
        state = GAME_STATE.GAME;
        Instance.countDownAnimator.SetTrigger("Start");
        Time.timeScale = 1f;
    }

    void onChangeScore()
    {
        scoreText.text = $"{((int)GameParams.score):D6}";
    }

    /// <summary>
    /// ゲームオーバーになったら呼び出します。
    /// </summary>
    public static void Gameover()
    {
        // すでにゲームオーバー以外なら何もしない
        if (state != GAME_STATE.GAME) return;

        state = GAME_STATE.GAMEOVER;
        GameParams.CheckHighScore();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameOver", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    /// <summary>
    /// クリアになったら呼び出します。
    /// </summary>
    public static void Clear()
    {
        // すでにクリアなら何もしない
        if (state == GAME_STATE.CLEAR) return;

        state = GAME_STATE.CLEAR;
        GameParams.CheckHighScore();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Clear", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
