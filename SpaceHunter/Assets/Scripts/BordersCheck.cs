using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersCheck : MonoBehaviour
{
    [Header("Set in Inspector")] 
    [SerializeField] private float repulsionRadius = 1f;

    [Header("Set Dynamically")] 
    private float camWight;
    private float camHeight;

    private void Awake()
    {
        Camera mainCamera = Camera.main;
        
        camHeight = mainCamera.orthographicSize;
        camWight = camHeight * mainCamera.aspect;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;

        if (position.x > camWight - repulsionRadius) position.x = camWight - repulsionRadius;
        if (position.x < -camWight + repulsionRadius) position.x = -camWight + repulsionRadius;
        if (position.y > camHeight - repulsionRadius) position.y = camHeight - repulsionRadius;
        if (position.y < -camHeight + repulsionRadius) position.y = -camHeight + repulsionRadius;
        
        transform.position = position;
    }
}
