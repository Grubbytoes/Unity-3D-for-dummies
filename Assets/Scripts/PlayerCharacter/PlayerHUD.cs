using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    private TextMeshPro geodeCount;
    private TextMeshPro tonicCount;

    void Awake()
    {
        geodeCount = transform.Find("Canvas/Item Panel/Geode Count").GetComponent<TextMeshPro>();

        if (geodeCount == null) Debug.Log("There was a problem!!");
    }
}
