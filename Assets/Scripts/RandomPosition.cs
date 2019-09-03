using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RandomPosition : MonoBehaviour
{
    [Tooltip("プレイヤーからの最低距離"), SerializeField]
    float minRange = 3f;
    [Tooltip("フリーズ防止のランダム試行回数"), SerializeField]
    int maxTry = 100;

    float width;
    float height;

    void Awake()
    {
        Camera cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
        SphereCollider col = GetComponent<SphereCollider>();
        float colliderRadius = col.radius;
        width -= colliderRadius;
        height -= colliderRadius;

        SetPosition();        
    }

    /// <summary>
    /// プレイヤーから離しておく距離を設定します。
    /// </summary>
    public void SetPosition()
    {
        Vector3 npos = Vector3.zero;
        Transform pl = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i=0; i<maxTry;i++)
        {
            npos.Set(Random.Range(-width, width), Random.Range(-height, height), 0);
            if (Vector3.Distance(pl.position, npos) >= minRange)
            {
                transform.position = npos;
                return;
            }
        }

#if UNITY_EDITOR
        Debug.LogWarning("座標設定に失敗しました。minRangeの値を小さくしてください。", gameObject);
#endif
        transform.position = npos;
    }
}
