using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Version : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI vt = GetComponent<TextMeshProUGUI>();
        vt.text = $"Ver {Application.version}";
    }
}
