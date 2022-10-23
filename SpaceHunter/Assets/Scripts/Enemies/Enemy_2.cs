using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_2 : Enemy
{
    [Header("Set in Inspector: Enemy_2")] 
    [SerializeField] private float sinEccentricity = 0.6f;
    [SerializeField] private float lifeTime = 10;

    [Header("Set Dynamically: Enemy_2")] 
    private Vector3 p0;
    private Vector3 p1;
    private float birthTime;

    private void Start()
    {
        p0 = Vector3.zero;
        p0.x = -bordersCheck.CamWight - bordersCheck.RepulsionRadius;
        p0.y = Random.Range(-bordersCheck.CamHeight, bordersCheck.CamHeight);
        
        p1 = Vector3.zero;
        p1.x = bordersCheck.CamWight + bordersCheck.RepulsionRadius;
        p1.y = Random.Range(-bordersCheck.CamHeight, bordersCheck.CamHeight);

        if (Random.value > 0.5f)
        {
            p0.x *= -1;
            p1.x *= -1;
        }

        birthTime = Time.time;
    }

    protected override void Move()
    {
        float uBeziers = (Time.time - birthTime) / lifeTime;

        if (uBeziers > 1)
        {
            Destroy(gameObject);
            return;
        }

        uBeziers = uBeziers + sinEccentricity * (Mathf.Sin(uBeziers * Mathf.PI * 2));

        Position = (1 - uBeziers) * p0 + uBeziers * p1;
    }
}
