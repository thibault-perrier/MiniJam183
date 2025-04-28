using UnityEngine;

public class TooltipOrder : MonoBehaviour
{
    public static TooltipOrder TOInstance;

    [SerializeField] private GameObject _tooltipBG;
    [SerializeField] private GameObject _tooltipText;

    void Awake()
    {
        if (TOInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        TOInstance = this;
    }

    public void ShowTooltip()
    {
        _tooltipBG.SetActive(true);
    }

    public void HideTooltip()
    {
        _tooltipBG.SetActive(false);
    }

    public void SetTooltipText(string text)
    {
        _tooltipText.GetComponent<TMPro.TMP_Text>().text = text;
    }
}
