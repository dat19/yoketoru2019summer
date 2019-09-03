using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameParams
{
    /// <summary>
    /// 最高得点
    /// </summary>
    const float SCORE_MAX = 999999;

    public static float score { get; private set; }
    public static int highScore { get; private set; }
    public static bool isHighScore { get; private set; }

    #region Private Variables

    static event UnityAction _onChangeScore;

    static bool isInited = false;

    #endregion Private Variables

    /// <summary>
    /// スコアを更新した時に呼び出すハンドラーを設定します。
    /// </summary>
    /// <param name="scoringHandle">引数、戻り値なしのメソッド</param>
    public static void SetChangeScore(UnityAction scoringHandle)
    {
        _onChangeScore = scoringHandle;
        _onChangeScore?.Invoke();
    }

    /// <summary>
    /// スコアを加算します。
    /// </summary>
    /// <param name="add">加算する得点</param>
    public static void AddScore(float add)
    {
        score += add;
        if (score > SCORE_MAX)
        {
            score = SCORE_MAX;
        }
        _onChangeScore?.Invoke();
    }

    /// <summary>
    /// スコアを0にします。
    /// </summary>
    public static void ClearScore()
    {
        if (!isInited)
        {
            isInited = true;
            highScore = 0;
        }

        score = 0;
        isHighScore = false;
        _onChangeScore?.Invoke();
    }

    /// <summary>
    /// ハイスコアチェック
    /// </summary>
    public static void CheckHighScore()
    {
        int sc = (int)score;
        if (sc > highScore)
        {
            highScore = sc;
            isHighScore = true;
        }
    }

}
