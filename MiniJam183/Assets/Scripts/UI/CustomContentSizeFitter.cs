using System;
using UnityEngine;

[ExecuteInEditMode]
public class CustomContentSizeFitter : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private RectTransform targetRectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!targetRectTransform)
        {
            return;
        }
        
        rectTransform.sizeDelta = targetRectTransform.sizeDelta;
    }
}
