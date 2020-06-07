using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelButton : MonoBehaviour
{
    public static RequestNewlevel requestNewLevel = new RequestNewlevel();

    public void RequestLevel(int levelNum)
    {
        requestNewLevel.Invoke(levelNum);
    }
}
