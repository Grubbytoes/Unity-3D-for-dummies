using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public int GeodeCount
    {
        get => _geodeCount;
        set {
            _geodeCount = value;
            geodeCountMesh.text = value.ToString();
        }
    } private int _geodeCount;
    public int TonicCount 
    {
        get => _tonicCount;
        set {
            _tonicCount = value;
            tonicCountMesh.text = value.ToString();
        }        
    }   private int _tonicCount;

    [SerializeField] private TextMeshProUGUI geodeCountMesh;
    [SerializeField] private TextMeshProUGUI tonicCountMesh;

    void Awake()
    {
        TonicCount = 0;
        GeodeCount = 0;
    }

    public void OnItemPickedUp(string item)
    {
        if (item == "tonic")
        {
            TonicCount++;
        }   
        else if (item == "geode")
        {
            GeodeCount++;
        }

        Debug.Log($"Unknown collectable '{item}'");
    }

    struct TextMeshNumber
    {
        int number;
        TextMeshProUGUI textMesh;
    }
}
