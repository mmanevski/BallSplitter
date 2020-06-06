using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipActivator : MonoBehaviour
{
    public bool activateOnFlip = false; 
    private void Awake()
    {
        PlayAreaController.playAreaFlipped.AddListener(OnPlayAreaFlipped);
    }

    private void Start()
    {
        gameObject.SetActive(!activateOnFlip);
    }

    private void OnPlayAreaFlipped()
    {
        gameObject.SetActive(activateOnFlip);
    }

    private void OnDestroy()
    {
        PlayAreaController.playAreaFlipped.RemoveListener(OnPlayAreaFlipped);
    }
}
