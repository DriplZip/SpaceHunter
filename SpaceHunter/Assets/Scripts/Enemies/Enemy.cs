using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float showDamageDuration = 0.1f;
    public float powerUpDropChance = 1f;
    
    [Header("Set in Inspector")]
    [SerializeField]
    private float speed = 10f;

    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _health = 10f;
    [SerializeField] private int _score = 100;

    [Header("Set Dynamically: Enemy")] 
    private Color[] _originalColors;
    private Material[] _materials;
    private float _damageDoneTime;
    private bool _showingDamage = false;
    private bool _notifiedOfDestruction = false;

    protected BordersCheck _bordersCheck;

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Awake()
    {
        _bordersCheck = GetComponent<BordersCheck>();

        _materials = Utils.GetAllMaterials(gameObject);
        _originalColors = new Color[_materials.Length];
        for (int i = 0; i < _materials.Length; i++)
        {
            _originalColors[i] = _materials[i].color;
        }
    }

    private void Update()
    {
        Move();
        
        if (_showingDamage && Time.time > _damageDoneTime) UnShowDamage();

        if (_bordersCheck != null && !_bordersCheck.IsOnScreen)
            if (Position.y < _bordersCheck.CamHeight - _bordersCheck.RepulsionRadius)
                Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile projectile = other.GetComponent<Projectile>();
                if (!_bordersCheck.IsOnScreen)
                {
                    Destroy(other);
                    break;
                }

                _health -= Main.GetWeaponDefinition(projectile.WeaponType).damageOnHit;
                if (_health <= 0)
                {
                    if (!_notifiedOfDestruction)
                    {
                        Main.S.ShipDestroyed(this);
                    }

                    _notifiedOfDestruction = true;
                    
                    Destroy(this.gameObject);
                }
                
                ShowDamage();
                
                Destroy(other);
                break;
            default:
                print("Enemy hit by non-ProjectileHero: " + other.name);
                break;
        }
    }

    protected virtual void Move()
    {
        Vector3 tmpPosition = Position;

        tmpPosition.y -= speed * Time.deltaTime;

        Position = tmpPosition;
    }

    private void ShowDamage()
    {
        foreach (Material material in _materials)
        {
            material.color = Color.red;
        }

        _showingDamage = true;
        _damageDoneTime = Time.time + showDamageDuration;
    }
    
    private void UnShowDamage()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].color = _originalColors[i];
        }
        
        _showingDamage = false;
    }
}