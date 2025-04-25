using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FastForwardButton : MonoBehaviour
{
    private Button _ffButton;

    private string[] _ffButtonTexts = { "x1", "x2", "x5" };
    private int[] _ffSpeeds = { 1, 2, 5 };

    private int _currentIndex = 0;

    private void Start()
    {
        _ffButton = GetComponentInChildren<Button>();
        _ffButton.GetComponentInChildren<TextMeshProUGUI>().text = _ffButtonTexts[_currentIndex];
        Time.timeScale = _ffSpeeds[_currentIndex];
    }

    public void NextSpeed()
    {
        _currentIndex = (_currentIndex + 1) % _ffButtonTexts.Length;
        _ffButton.GetComponentInChildren<TextMeshProUGUI>().text = _ffButtonTexts[_currentIndex];
        Time.timeScale = _ffSpeeds[_currentIndex];

        Debug.Log("Time Scale: " + Time.timeScale);
    }
}
