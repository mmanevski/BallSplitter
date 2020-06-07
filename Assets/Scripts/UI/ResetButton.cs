using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetButton : MonoBehaviour
{
    public static UnityEvent requestLevelReset = new UnityEvent();
    
    public void OnClick()
    {
        requestLevelReset.Invoke();
    }
}
