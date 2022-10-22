using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersCheck : MonoBehaviour
{
    [Header("Set in Inspector")] 
    [SerializeField] private float repulsionRadius = 3f;
    [SerializeField] private bool keepOnScreen = true;

    public float RepulsionRadius => repulsionRadius;
    
    [Header("Set Dynamically")]
    private bool isOnScreen = true;
    private float camWight;
    private float camHeight;

    public float CamHeight => camHeight;

    public bool IsOnScreen => isOnScreen;

    private void Awake()
    {
        Camera mainCamera = Camera.main;
        
        camHeight = mainCamera.orthographicSize;
        camWight = camHeight * mainCamera.aspect;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        isOnScreen = true;
        
        if (position.x > camWight - repulsionRadius)
        {
            position.x = camWight - repulsionRadius;
            isOnScreen = false;
        }

        if (position.x < -camWight + repulsionRadius)
        {
            position.x = -camWight + repulsionRadius;
            isOnScreen = false;
        }

        if (position.y > camHeight - repulsionRadius)
        {
            position.y = camHeight - repulsionRadius;
            isOnScreen = false;
        }

        if (position.y < -camHeight + repulsionRadius)
        {
            position.y = -camHeight + repulsionRadius;
            isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = position;
            isOnScreen = true;
        }
    }
}
