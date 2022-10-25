using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_3 : Enemy
{
    [Header("Set in Inspector: Enemy_3")]
    [SerializeField] private float lifeTime = 5;

    [Header("Set Dynamically: Enemy_3")] 
    private Vector3[] points;
    private float birthTime;

    private void Start()
    {
        points = new Vector3[3];
        points[0] = Position;

        float xMin = -bordersCheck.CamWight + bordersCheck.RepulsionRadius;
        float xMax = bordersCheck.CamWight - bordersCheck.RepulsionRadius;

        Vector3 point = Vector3.zero;
        point.x = Random.Range(xMin, xMax);
        point.y = -bordersCheck.CamHeight * Random.Range(2f, 2.5f);
        
        points[1] = point;
        
        point = Vector3.zero;
        point.y = Position.y;
        point.x = Random.Range(xMin, xMax);

        points[2] = point;

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

        Vector3 p01, p12;
        uBeziers -= 0.2f * Mathf.Sin(uBeziers * Mathf.PI * 2); // Beziers smoothing
        
        p01 = (1 - uBeziers) * points[0] + uBeziers * points[1];
        p12 = (1 - uBeziers) * points[1] + uBeziers * points[2];

        Position = (1 - uBeziers) * p01 + uBeziers * p12;
    }
}
