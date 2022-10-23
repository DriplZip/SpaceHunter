using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")] 
    [SerializeField] private float waveFrequency = 2;
    [SerializeField] private float waveWidth = 4;
    [SerializeField] private float waveRotationY = 10;

    private float x0;
    private float birthTime;

    private void Start()
    {
        x0 = Position.x;

        birthTime = Time.time;
    }

    protected override void Move()
    {
        Vector3 position = Position;

        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        position.x = x0 + waveWidth * sin;
        Position = position;

        Vector3 rotation = new Vector3(0, sin * waveRotationY, 0);
        transform.rotation = Quaternion.Euler(rotation);
        
        base.Move();
    }
}
