using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersCheck : MonoBehaviour
{
    [Header("Set in Inspector")] 
    [SerializeField] private float repulsionRadius = 3f;
    [SerializeField] private bool keepOnScreen = true;
    
    [Header("Set Dynamically")]
    private bool isOnScreen = true;
    private float camWight;
    private float camHeight;

    public bool OffRight { get; private set; }
    public bool OffLeft { get; private set; }
    public bool OffUp { get; private set; }
    public bool OffDown { get; private set; }

    public float RepulsionRadius => repulsionRadius;

    public float CamHeight => camHeight;
    public float CamWight => camWight;

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
        OffDown = OffLeft = OffRight = OffUp = false;
        
        if (position.x > camWight - repulsionRadius)
        {
            position.x = camWight - repulsionRadius;
            OffRight = true;
        }

        if (position.x < -camWight + repulsionRadius)
        {
            position.x = -camWight + repulsionRadius;
            OffLeft = true;
        }

        if (position.y > camHeight - repulsionRadius)
        {
            position.y = camHeight - repulsionRadius;
            OffUp = true;
        }

        if (position.y < -camHeight + repulsionRadius)
        {
            position.y = -camHeight + repulsionRadius;
            OffDown = true;
        }

        isOnScreen = !(OffDown || OffLeft || OffRight || OffUp);
        
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = position;
            isOnScreen = true;
            
            OffDown = OffLeft = OffRight = OffUp = false;
        }
    }
}
