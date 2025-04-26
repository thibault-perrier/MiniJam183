using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button _lButton;
    [SerializeField] private Button _rButton;

    private RectTransform _rectTransform;
    private HorizontalLayoutGroup _layout;
    private float _sizeOfLevel;


    private int _currentIndex = 0;
    private int _totalLevels = 0;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _layout = GetComponent<HorizontalLayoutGroup>();
        _sizeOfLevel = _rectTransform.GetChild(0).GetComponent<RectTransform>().rect.width;
    }

    private void Start()
    {
        _totalLevels = _rectTransform.childCount;
        UpdatePosition();
        PreviousLevel();
    }

    public void NextLevel()
    {
        if (_currentIndex < _totalLevels - 1)
        {
            _currentIndex++;
            _lButton.interactable = true;
            UpdatePosition();
        }
        if (_currentIndex == _totalLevels - 1)
        {
            _rButton.interactable = false;
        }
    }


    public void PreviousLevel()
    {
        if(_currentIndex > 0)
        {
            _currentIndex--;
            _rButton.interactable = true;
            UpdatePosition();
        }
        if (_currentIndex == 0)
        {
            _lButton.interactable = false;
        }
    }

    private void UpdatePosition()
    {
        _rectTransform.anchoredPosition = new Vector3(-_currentIndex * (_layout.spacing + _sizeOfLevel), _rectTransform.position.y, _rectTransform.position.z);
        //Debug.Log(" Current index = " + _currentIndex + ", Layout spacing " +  _layout.spacing + ", Size of level " + _sizeOfLevel);
        //Debug.Log("-Index * (spacing + sizeOfLevel) = " +  -_currentIndex * (_layout.spacing + _sizeOfLevel));
    }
}
