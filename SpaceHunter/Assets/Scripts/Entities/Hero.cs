using UnityEngine;

public class Hero : MonoBehaviour
{
    private const int MaxShieldLvl = 4;
    [SerializeField] private static Hero _s;

    [Header("Set in Inspector")]
    [SerializeField] private float _speed = 30;
    [SerializeField] private float _rollMulti = -45;
    [SerializeField] private float _rotationMulti = 30;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 40;
    public Weapon[] weapons;

    private readonly float _gameRestartDelay = 2f;

    private GameObject _lastTriggerEnemy;
    private int _shieldLvl = 1;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    public static Hero S => _s;

    public int ShieldLvl
    {
        get => _shieldLvl;
        private set
        {
            _shieldLvl = Mathf.Min(value, MaxShieldLvl);
            if (value < 0)
            {
                Destroy(gameObject);

                GameRestart.S.DelayedRestart(_gameRestartDelay);
            }
        }
    }

    private void Awake()
    {
        if (_s == null)
            _s = this;
        else
            Debug.LogError("Hero.Awake() - Attempt to create not a single hero");

        //fireDelegate += StartShooting;
    }

    private void Update()
    {
        var xAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");

        var position = transform.position;
        position.x += xAxis * _speed * Time.deltaTime;
        position.y += yAxis * _speed * Time.deltaTime;
        transform.position = position;

        transform.rotation = Quaternion.Euler(yAxis * _rotationMulti, xAxis * _rotationMulti, 0);

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null) fireDelegate();
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject enemy = rootTransform.gameObject;

        if (enemy == _lastTriggerEnemy) return;

        _lastTriggerEnemy = enemy;

        if (enemy.CompareTag("Enemy"))
        {
            ShieldLvl--;
            Destroy(enemy);
        }
        else if (enemy.CompareTag("PowerUp"))
        {
            AbsorbPowerUp(enemy);
        }
        else
        {
            print("Triggered by non-Enemy: " + enemy.name);
        }
    }

    private void StartShooting()
    {
        GameObject projectilePref = Instantiate(_projectilePrefab);
        projectilePref.transform.position = transform.position;

        Rigidbody rigidbodyProjectile = projectilePref.GetComponent<Rigidbody>();

        Projectile projectile = projectilePref.GetComponent<Projectile>();
        projectile.WeaponType = WeaponType.blaster;

        float speed = EnemySpawner.GetWeaponDefinition(projectile.WeaponType).Velocity;
        
        rigidbodyProjectile.velocity = Vector3.up * speed;
    }

    public void AbsorbPowerUp(GameObject target)
    {
        PowerUp powerUp = target.GetComponent<PowerUp>();

        switch (powerUp.bonusType)
        {
            case WeaponType.shield:
                if (_shieldLvl == 4) break;
                
                _shieldLvl++;
                break;
            
            default:
                if (powerUp.bonusType == weapons[0].Type)
                {
                    Weapon weapon = GetEmptyWeaponSlot();
                    
                    if (weapon != null)
                    {
                        weapon.Type = powerUp.bonusType;
                    }
                }
                else
                {
                    ClearWeapons();
                    weapons[0].Type = powerUp.bonusType;
                }
                break;
        }
        
        powerUp.AbsorbedBy(this.gameObject);
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].Type == WeaponType.none) return weapons[i];
        }

        if (weapons.Length == 5) return weapons[weapons.Length - 1];

        return null;
    }

    void ClearWeapons()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.Type = WeaponType.none;
        }
    }
}