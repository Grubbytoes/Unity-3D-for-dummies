using System;
using System.IO;
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

    private static readonly string textFilePath = "Assets/Media/Text";
    public Noisemaker paperSfx;

    [SerializeField] private TextMeshProUGUI GeodeCountMesh;
    [SerializeField] private TextMeshProUGUI TonicCountMesh;
    [SerializeField] private TextMeshProUGUI LongMessageMesh;
    [SerializeField] private TooltipPanel tooltipPanel;

    protected Animator anim;
    protected bool popupActive;
    protected StreamReader textFileReader;

    void Awake()
    {
        TonicCount = 0;
        GeodeCount = 0;
        anim = GetComponent<Animator>();

        // Subscribing to popup events
        InteractObject.PopupText += Popup;
        PlayerCharacter.UiEscape += EscapePopup;

        // Subscribing to the tooltip event
        InteractObject.DisplayTooltip += ShowTooltip;
    }

    private void Popup(string text, bool asPath)
    {
        // Play sfx
        paperSfx.MakeNoise();

        // toggle
        if (popupActive)
        {
            anim.SetTrigger("putPaperDown");
            popupActive = false;
            return;
        }

        // Set text
        string popupText;
        if (asPath)
        {
            textFileReader = new($"{textFilePath}/{text}.txt");
            popupText = textFileReader.ReadToEnd();
        }
        else
        {
            popupText = text;
        }

        LongMessageMesh.text = popupText;

        anim.SetTrigger("putPaperUp");
        popupActive = true;
    }

    private void EscapePopup()
    {
        if (!popupActive) return;

        anim.SetTrigger("putPaperDown");
        popupActive = false;
    }

    private void ShowTooltip(string tooltip)
    {
        if (tooltip == "")
        {
            Debug.Log("should be hiding tooltip panel...");
            if (tooltip != null) tooltipPanel.Hide();
            return;
        }

        tooltipPanel.enabled = true;
        tooltipPanel.Show(tooltip);
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
