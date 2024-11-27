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
            GeodeCountMesh.text = value.ToString();
        }
    } private int _geodeCount;
    public int TonicCount 
    {
        get => _tonicCount;
        set {
            _tonicCount = value;
            TonicCountMesh.text = value.ToString();
        }        
    }   private int _tonicCount;

    [SerializeField] private TextMeshProUGUI GeodeCountMesh;
    [SerializeField] private TextMeshProUGUI TonicCountMesh;

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
        else
        {
            Debug.Log($"Unknown collectable '{item}'");
        }
    }
}
