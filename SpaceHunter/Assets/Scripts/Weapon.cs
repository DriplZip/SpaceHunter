using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    none,
    blaster,
    spread,
    shield
}

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public GameObject projectilePrefab;
    
    [SerializeField] private string letter;
    
    [SerializeField] private Color colorBonus = Color.white;
    [SerializeField] private Color projectileColor = Color.white;
    
    [SerializeField] private float damageOnHit = 0;
    [SerializeField] private float continuousDamage = 0;
    [SerializeField] private float delayBetweenShots = 0;
    [SerializeField] private float velocity = 20;
}

public class Weapon : MonoBehaviour
{
   
}
