using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Data/GameParameters", order = 0)]
public class GameParameters : ScriptableObject
{
    public float minBallScale = 0.125f;
    public float maxBallScale = 0.5f;
    public float moveChangeRange = 0.4f; //range in which swipe movement is registered for control
    public float forceMultiplier = 10f; //force multiplier for ball control
    public float rotationFactor = 30f; //rotation factor for controlling rotation of play area
    
}
