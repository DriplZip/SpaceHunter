using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    private static BonusSpawner _s;
    private static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;

    [SerializeField] private WeaponDefinition[] _weaponDefinitions;

    private BordersCheck _bordersCheck;

    private void Awake()
    {
        _s = this;

        _bordersCheck = GetComponent<BordersCheck>();

        WEAPON_DICT = new Dictionary<WeaponType, WeaponDefinition>();

        foreach (WeaponDefinition definition in _weaponDefinitions) WEAPON_DICT[definition.type] = definition;
    }
    
    public static WeaponDefinition GetWeaponDefinition(WeaponType weaponType)
    {
        if (WEAPON_DICT.ContainsKey(weaponType)) return WEAPON_DICT[weaponType];

        return new WeaponDefinition();
    }
}