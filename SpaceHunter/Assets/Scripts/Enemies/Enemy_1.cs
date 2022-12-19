using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")] [SerializeField]
    private float _waveFrequency = 2;

    [SerializeField] private float _waveWidth = 4;
    [SerializeField] private float _waveRotationY = 10;
    private float _birthTime;

    private float _x0;

    private void Start()
    {
        _x0 = Position.x;

        _birthTime = Time.time;
    }

    protected override void Move()
    {
        Vector3 position = Position;

        float age = Time.time - _birthTime;
        float theta = Mathf.PI * 2 * age / _waveFrequency;
        float sin = Mathf.Sin(theta);
        position.x = _x0 + _waveWidth * sin;
        Position = position;

        Vector3 rotation = new Vector3(0, sin * _waveRotationY, 0);
        transform.rotation = Quaternion.Euler(rotation);

        base.Move();
    }
}