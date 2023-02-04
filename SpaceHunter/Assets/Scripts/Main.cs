using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    static public Main S;
    
    public WeaponDefinition[] WeaponDefinitions;

    public WeaponType[] powerUpFrequency = new WeaponType[]
        { WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield };
    
    public GameObject prefabPowerUp;
    
    private static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;
    private BordersCheck _bordersCheck;

    private void Awake()
    {
        S = this;
        WEAPON_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition weaponDefinition in WeaponDefinitions)
        {
            WEAPON_DICT[weaponDefinition.type] = weaponDefinition;
        }
    }

    public void ShipDestroyed(Enemy enemy)
    {
        if (Random.value <= enemy.powerUpDropChance)
        {
            int idx = Random.Range(0, powerUpFrequency.Length);
            WeaponType powerUpType = powerUpFrequency[idx];
            
            GameObject gameObjectPowerUp = Instantiate(prefabPowerUp) as GameObject;
            PowerUp powerUp = gameObjectPowerUp.GetComponent<PowerUp>();

            powerUp.SetBonusType(powerUpType);

            powerUp.transform.position = enemy.transform.position;
        }
    }
    
    static public WeaponDefinition GetWeaponDefinition(WeaponType type)
    {
        if (WEAPON_DICT.ContainsKey(type)) return WEAPON_DICT[type];

        return new WeaponDefinition();
    }
}
