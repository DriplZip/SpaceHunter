using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    private static BonusSpawner s;
    private static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;

    [SerializeField] private WeaponDefinition[] weaponDefinitions;

    private BordersCheck bordersCheck;

    private void Awake()
    {
        s = this;

        bordersCheck = GetComponent<BordersCheck>();

        WEAPON_DICT = new Dictionary<WeaponType, WeaponDefinition>();

        foreach (WeaponDefinition definition in weaponDefinitions)
        {
            WEAPON_DICT[definition.type] = definition;
        }
    }
}
