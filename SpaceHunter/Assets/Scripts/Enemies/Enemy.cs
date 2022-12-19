using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")] [SerializeField]
    private float speed = 10f;

    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _health = 10f;
    [SerializeField] private int _score = 100;

    protected BordersCheck _bordersCheck;

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Awake()
    {
        _bordersCheck = GetComponent<BordersCheck>();
    }

    private void Update()
    {
        Move();

        if (_bordersCheck != null && !_bordersCheck.IsOnScreen)
            if (Position.y < _bordersCheck.CamHeight - _bordersCheck.RepulsionRadius)
                Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("ProjectileHero"))
        {
            Destroy(other);
            Destroy(gameObject);
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + other.name);
        }
    }

    protected virtual void Move()
    {
        Vector3 tmpPosition = Position;

        tmpPosition.y -= speed * Time.deltaTime;

        Position = tmpPosition;
    }
}