using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Set Dynamically")] 
    [SerializeField] private WeaponType _weaponType;

    private BordersCheck _bordersCheck;
    private Renderer _renderer;
    public Rigidbody Rigidbody { get; private set; }

    public WeaponType WeaponType
    {
        get => _weaponType;
        set => SetWeaponType(value);
    }

    private void Awake()
    {
        _bordersCheck = GetComponent<BordersCheck>();
        _renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_bordersCheck.OffUp) Destroy(gameObject);
    }

    private void SetWeaponType(WeaponType type)
    {
        _weaponType = type;

        WeaponDefinition weaponDefinition = EnemySpawner.GetWeaponDefinition(_weaponType);

        _renderer.material.color = weaponDefinition.ProjectileColor;
    }
}