using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Bounder : MonoBehaviour
{
    #region Private Variables

    static Bounds screenBounds;
    static bool isInited = false;

    SphereCollider sphereCollider = null;
    Rigidbody rb = null;

    #endregion Private Variables

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();

        if (!isInited)
        {
            isInited = true;
            Camera cam = Camera.main;
            Vector3 ex = new Vector3(cam.orthographicSize*cam.aspect, cam.orthographicSize);
            screenBounds.extents = ex;
        }
    }

    private void FixedUpdate()
    {
        Vector3 v = rb.velocity;

        // 左右に出ていないか
        if (sphereCollider.bounds.min.x < screenBounds.min.x)
        {
            v.x = Mathf.Abs(v.x);
        }
        else if (sphereCollider.bounds.max.x > screenBounds.max.x)
        {
            v.x = -Mathf.Abs(v.x);
        }
        // 上下に出ていないか
        if (sphereCollider.bounds.min.y < screenBounds.min.y)
        {
            v.y = Mathf.Abs(v.y);
        }
        else if(sphereCollider.bounds.max.y > screenBounds.max.y)
        {
            v.y = -Mathf.Abs(v.y);
        }

        rb.velocity = v;
    }
}
