using TMPro;
using UnityEngine;
using UnityEngine.UI;

class TooltipPanel : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textMesh;
    protected Image image; 

    void Awake()
    {
        image = GetComponent<Image>();

        Debug.Log(image == null);

        Hide();
    }

    public void Show(string s)
    {
        if (image == null || textMesh == null) return;

        image.enabled = true;
        textMesh.enabled = true;

        textMesh.text = s;
    }

    public void Hide() 
    {
        if (image == null || textMesh == null) return;

        image.enabled = false;
        textMesh.enabled = false;
    }
}