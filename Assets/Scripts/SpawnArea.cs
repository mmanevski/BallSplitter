﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public float rightBoundry;
    public float upperBoundry;

    private Collider spawnAreaCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnAreaCollider = GetComponent<Collider>();
        //TODO: find out how the boundries actually should work
        rightBoundry = transform.position.z + spawnAreaCollider.bounds.size.z;
        upperBoundry = transform.position.y + spawnAreaCollider.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
