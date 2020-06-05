using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelFailedPanel : MonoBehaviour
{
    public Button retryLevelButton;
    public static UnityEvent requestLevelReset = new UnityEvent();

    private void Awake()
    {
        retryLevelButton.onClick.AddListener(RetryLevelPressed);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    
    public void RetryLevelPressed()
    {
        requestLevelReset.Invoke();
    }

    private void OnDestroy()
    {
        retryLevelButton.onClick.RemoveListener(RetryLevelPressed);
    }
}
