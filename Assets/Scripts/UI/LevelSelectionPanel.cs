using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

public class LevelSelectionPanel : MonoBehaviour
{
    public void Toggle(bool toggle)
    {
        GameManager.gameState = toggle? GameState.Loading : GameState.Playing;
        gameObject.SetActive(toggle);
    }

}
