using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Vector3 moveToPos;
    public float moveTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = transform.position;
        LeanTween.moveLocal(gameObject, moveToPos, moveTime).setEaseInOutExpo().setOnComplete(() =>
        {
            LeanTween.moveLocal(gameObject, startPos, moveTime).setEaseInOutExpo();
        }).setLoopPingPong();
    }

}
