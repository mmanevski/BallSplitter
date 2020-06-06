using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            other.GetComponent<BallController>().Despawn();

        }
    }
}
