using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private float rotationPerSecond = 0.1f;
    
    [Header("Set Dynamically")]
    private int shieldLvlShown = 0;

    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        int currentShieldLvl = Hero.S.ShieldLvl;

        if (shieldLvlShown != currentShieldLvl)
        {
            shieldLvlShown = currentShieldLvl;

            material.mainTextureOffset = new Vector2(0.2f * shieldLvlShown, 0);
        }

        float rotationZ = -(rotationPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
