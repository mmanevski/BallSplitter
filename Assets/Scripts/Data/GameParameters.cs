using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Data/GameParameters", order = 0)]
public class GameParameters : ScriptableObject
{
    public float moveChangeRange = 0.4f; //range in which swipe movement is registered for control
    public float forceMultiplier = 10f; //force multiplier for ball control
    
}
