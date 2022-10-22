using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BordersCheck bordersCheck;

    private void Awake()
    {
        bordersCheck = GetComponent<BordersCheck>();
    }

    private void Update()
    {
        if (bordersCheck.OffUp) Destroy(gameObject);
    }
}
