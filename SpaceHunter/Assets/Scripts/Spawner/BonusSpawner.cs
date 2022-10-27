using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    private static BonusSpawner s;

    [Header("Set in Inspector")] [SerializeField]
    private GameObject[] enemiesPrefab;

    [SerializeField] private float enemySpawnPerSecond = 0.5f;
    [SerializeField] private float enemyDefaultPadding = 1.5f;
    [SerializeField] private WeaponDefinition[] weaponDefinitions;

    private BordersCheck bordersCheck;

    private void Awake()
    {
        s = this;

        bordersCheck = GetComponent<BordersCheck>();

        Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }

    private void SpawnEnemy()
    {
        int enemyPrefabIdx = Random.Range(0, enemiesPrefab.Length);
        GameObject enemy = Instantiate(enemiesPrefab[enemyPrefabIdx]);
        BordersCheck enemyBordersCheck = enemy.GetComponent<BordersCheck>();

        float enemyPadding = enemyDefaultPadding;
        if (enemyBordersCheck != null) enemyPadding = Mathf.Abs(enemyBordersCheck.RepulsionRadius);

        Vector3 position = Vector3.zero;
        float xMin = -bordersCheck.CamWight + enemyPadding;
        float xMax = bordersCheck.CamWight - enemyPadding;

        position.x = Random.Range(xMin, xMax);
        position.y = bordersCheck.CamHeight + enemyPadding;

        enemy.transform.position = position;
        
        Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }
}
