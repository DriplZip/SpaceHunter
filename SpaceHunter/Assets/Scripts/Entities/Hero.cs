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

    private const int maxShieldLvl = 4;
    private int shieldLvl = 1;

    private GameObject lastTriggerEnemy = null;

    private float gameRestartDelay = 2f;

    public static Hero S => s;

    public int ShieldLvl
    {
        get => shieldLvl;
        private set 
        { 
            shieldLvl = Mathf.Min(value, maxShieldLvl);

            if (value < 0)
            {
                Destroy(gameObject);
                
                GameRestart.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

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

    private void OnTriggerEnter(Collider other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject enemy = rootTransform.gameObject;
        
        if (enemy == lastTriggerEnemy) return;

        lastTriggerEnemy = enemy;

        if (enemy.CompareTag("Enemy"))
        {
            Destroy(enemy);
            ShieldLvl--;
        }
        else
        {
            print("Triggered by non-Enemy: "+ enemy.name);
        }
    }
}
