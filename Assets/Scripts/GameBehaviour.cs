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
    }

    void Start()
    {
        _countDown = 3;
        GameParams.SetChangeScore(onChangeScore);
        GameParams.ClearScore();
        countDownText.text = _countDown.ToString();
        countDownAnimator.SetTrigger("CountDown");
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
            Instance.countDownText.text = "START!!";
            state = GAME_STATE.GAME;
            Instance.countDownAnimator.SetTrigger("Start");
        }
        else
        {
            Instance.countDownAnimator.gameObject.transform.root.gameObject.SetActive(false);
        }
    }

    void onChangeScore()
    {
        scoreText.text = $"{GameParams.score:D6}";
    }
}
