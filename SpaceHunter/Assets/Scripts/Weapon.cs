using System;
using UnityEngine;

public enum WeaponType
{
    none,
    blaster,
    spread,
    shield
}

[Serializable]
public class WeaponDefinition
{
    [SerializeField] private string letter;

    [SerializeField] private Color colorBonus = Color.white;
    [SerializeField] private Color projectileColor = Color.white;

    [SerializeField] private float continuousDamage = 0;
    [SerializeField] private float velocity = 20;

    public WeaponType type = WeaponType.none;
    public GameObject projectilePrefab;

    public float damageOnHit = 0;
    public float delayBetweenShots = 0;
    public float Velocity => velocity;
    public string Letter => letter;
    public Color ProjectileColor => projectileColor;
    public Color ColorBonus => colorBonus;
}

public class Weapon : MonoBehaviour
{
    private static Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")]
    [SerializeField] private WeaponType _type = WeaponType.none;
    private WeaponDefinition _definition;
    private GameObject _muzzle;
    private float _lastShotTime;
    private Renderer _muzzleRenderer;

    public WeaponType Type
    {
        get => _type;
        set => SetWeaponType(value);
    }

    private void Start()
    {
        _muzzle = transform.Find("Muzzle").gameObject;
        _muzzleRenderer = _muzzle.GetComponent<Renderer>();
        
        SetWeaponType(_type);

        if (PROJECTILE_ANCHOR == null)
        {
            GameObject startOfProjectileFlight = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = startOfProjectileFlight.transform;
        }

        GameObject rootAnchor = transform.root.gameObject;
        if (rootAnchor.GetComponent<Hero>() != null) rootAnchor.GetComponent<Hero>().fireDelegate += Fire;
    }

    private void SetWeaponType(WeaponType type)
    {
        _type = type;

        if (_type == WeaponType.none)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        _definition = EnemySpawner.GetWeaponDefinition(_type);
        _muzzleRenderer.material.color = _definition.ProjectileColor;
        _lastShotTime = 0;
    }

    private void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Time.time - _lastShotTime < _definition.delayBetweenShots) return;

        Projectile projectile;
        Vector3 projectileVelocity = Vector3.up * _definition.Velocity;
        if (transform.up.y < 0) projectileVelocity.y = -projectileVelocity.y;

        switch (Type)
        {
            case WeaponType.blaster:
                projectile = MakeProjectile();
                projectile.Rigidbody.velocity = projectileVelocity;
                break;
            case WeaponType.spread:
                projectile = MakeProjectile();
                projectile.Rigidbody.velocity = projectileVelocity;
                projectile = MakeProjectile();
                projectile.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                projectile.Rigidbody.velocity = projectile.transform.rotation * projectileVelocity;
                projectile = MakeProjectile();
                projectile.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                projectile.Rigidbody.velocity = projectile.transform.rotation * projectileVelocity;
                break;
        }
    }

    public Projectile MakeProjectile()
    {
        GameObject projectilePrefab = Instantiate(_definition.projectilePrefab, PROJECTILE_ANCHOR, true);
        if (transform.parent.gameObject.CompareTag("Hero"))
        {
            projectilePrefab.tag = "ProjectileHero";
            projectilePrefab.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else
        {
            projectilePrefab.tag = "ProjectileEnemy";
            projectilePrefab.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }

        projectilePrefab.transform.position = _muzzle.transform.position;

        Projectile projectile = projectilePrefab.GetComponent<Projectile>();
        projectile.WeaponType = Type;
        
        _lastShotTime = Time.time;

        return projectile;
    }
}