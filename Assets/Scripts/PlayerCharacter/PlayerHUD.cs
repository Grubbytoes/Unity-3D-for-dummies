using System;
using TMPro;
using UnityEngine;

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

    protected Animator anim;
    protected bool popupActive;

    void Awake()
    {
        TonicCount = 0;
        GeodeCount = 0;
        anim = GetComponent<Animator>();

        InteractObject.PopupText += Popup;
        PlayerCharacter.UiEscape += EscapePopup;
    }

    private void Popup(string text, bool asPath)
    {
        anim.SetTrigger("putPaperUp");
        popupActive = true;
    }

    private void EscapePopup()
    {
        if (!popupActive) return;

        anim.SetTrigger("putPaperDown");
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
    }
}
