using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugButton : MonoBehaviour
{
    public static UnityEvent requestDebugMenu = new UnityEvent();
    
    public void OnClick()
    {
        requestDebugMenu.Invoke();
    }
}
