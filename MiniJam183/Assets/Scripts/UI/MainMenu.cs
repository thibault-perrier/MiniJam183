using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _levelSelectorPanel;

    public static bool HasToLoadLevelSelector = false;
    void Start()
    {
        if (HasToLoadLevelSelector)
        {
            _levelSelectorPanel.SetActive(true);
            _mainMenuPanel.SetActive(false);
            HasToLoadLevelSelector = false;
        }
    }
}
