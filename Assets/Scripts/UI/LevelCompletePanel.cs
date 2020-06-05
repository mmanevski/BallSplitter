using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    public Button nextLevelButton;
    public static UnityEvent requestNextLevel = new UnityEvent();

    private void Awake()
    {
        nextLevelButton.onClick.AddListener(NextLevelPressed);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    
    public void NextLevelPressed()
    {
        requestNextLevel.Invoke();
    }

    private void OnDestroy()
    {
        nextLevelButton.onClick.RemoveListener(NextLevelPressed);
    }
}
