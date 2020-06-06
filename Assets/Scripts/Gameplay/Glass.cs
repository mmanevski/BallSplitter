using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;
using Zenject;

public class Glass : MonoBehaviour
{
    [Inject] private GameParameters gameParameters;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            if (other.transform.localScale.x == gameParameters.maxBallScale)
            {
                gameObject.SetActive(false);
            }
            
        }
    }
}
