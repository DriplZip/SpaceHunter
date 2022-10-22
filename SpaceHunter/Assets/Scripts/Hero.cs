using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private static Hero s;

    [Header("Set in Inspector")]
    [SerializeField] private float speed = 30;
    [SerializeField] private float rollMulti = -45;
    [SerializeField] private float rotationMulti = 30;

    private int _shieldLvl = 1;

    public static Hero S => s;
    
    public int SheildLvl => _shieldLvl;

    private void Awake()
    {
        if (s == null)
            s = this;
        else
            Debug.LogError("Hero.Awake() - Attempt to create not a single hero");
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 position = transform.position;
        position.x += xAxis * speed * Time.deltaTime;
        position.y += yAxis * speed * Time.deltaTime;
        transform.position = position;

        transform.rotation = Quaternion.Euler(yAxis * rotationMulti, xAxis * rotationMulti, 0);
    }
}
