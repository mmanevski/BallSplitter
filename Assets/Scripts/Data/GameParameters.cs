using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Data/GameParameters", order = 0)]
public class GameParameters : ScriptableObject
{
    public float minBallScale = 0.25f;
    public float maxBallScale = 0.5f;
    public float flipAreaTreshold = 0.2f;
    public float moveChangeRange = 0.5f; //range in which swipe movement is registered for control
    public float forceMultiplier = 0.5f; //force multiplier for ball control
    public float rotationFactor = 40f; //rotation factor for controlling rotation of play area
    public float playAreaRotationRange = 20f;
}
