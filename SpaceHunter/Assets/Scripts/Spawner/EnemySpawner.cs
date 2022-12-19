using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _s;

    [Header("Set in Inspector")] 
    [SerializeField] private GameObject[] _enemiesPrefab;

    [SerializeField] private float _enemySpawnPerSecond = 0.5f;
    [SerializeField] private float _enemyDefaultPadding = 1.5f;

    public WeaponDefinition[] WeaponDefinitions;

    private static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;

    private BordersCheck _bordersCheck;

    private void Awake()
    {
        _s = this;

        _bordersCheck = GetComponent<BordersCheck>();

        Invoke(nameof(SpawnEnemy), 1f / _enemySpawnPerSecond);

        WEAPON_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition weaponDefinition in WeaponDefinitions)
        {
            WEAPON_DICT[weaponDefinition.type] = weaponDefinition;
        }
    }

    private void SpawnEnemy()
    {
        int enemyPrefabIdx = Random.Range(0, _enemiesPrefab.Length);
        GameObject enemy = Instantiate(_enemiesPrefab[enemyPrefabIdx]);
        BordersCheck enemyBordersCheck = enemy.GetComponent<BordersCheck>();

        float enemyPadding = _enemyDefaultPadding;
        if (enemyBordersCheck != null) enemyPadding = Mathf.Abs(enemyBordersCheck.RepulsionRadius);

        Vector3 position = Vector3.zero;
        float xMin = -_bordersCheck.CamWight + enemyPadding;
        float xMax = _bordersCheck.CamWight - enemyPadding;

        position.x = Random.Range(xMin, xMax);
        position.y = _bordersCheck.CamHeight + enemyPadding;

        enemy.transform.position = position;

        Invoke(nameof(SpawnEnemy), 1f / _enemySpawnPerSecond);
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType type)
    {
        if (WEAPON_DICT.ContainsKey(type)) return WEAPON_DICT[type];

        return new WeaponDefinition();
    }
}