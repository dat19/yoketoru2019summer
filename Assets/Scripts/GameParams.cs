using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameParams
{
    /// <summary>
    /// 最高得点
    /// </summary>
    const int SCORE_MAX = 999999;

    public static int score { get; private set; }

    static event UnityAction _onChangeScore;

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
    public static void AddScore(int add)
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
        score = 0;
        _onChangeScore?.Invoke();
    }

}
