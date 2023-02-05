using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Set in Inspector: Enemy_2")] [SerializeField]
    private float _sinEccentricity = 0.6f;

    [SerializeField] private float _lifeTime = 10;
    private float _birthTime;

    [Header("Set Dynamically: Enemy_2")] private Vector3 _p0;

    private Vector3 _p1;

    private void Start()
    {
        _p0 = Vector3.zero;
        _p0.x = -_bordersCheck.CamWight - _bordersCheck.RepulsionRadius;
        _p0.y = Random.Range(0, _bordersCheck.CamHeight);

        _p1 = Vector3.zero;
        _p1.x = _bordersCheck.CamWight + _bordersCheck.RepulsionRadius;
        _p1.y = Random.Range(-_bordersCheck.CamHeight, _bordersCheck.CamHeight);

        if (Random.value > 0.5f)
        {
            _p0.x *= -1;
            _p1.x *= -1;
        }

        _birthTime = Time.time;
    }

    protected override void Move()
    {
        float uBeziers = (Time.time - _birthTime) / _lifeTime;

        if (uBeziers > 1)
        {
            Destroy(gameObject);
            return;
        }

        uBeziers = uBeziers + _sinEccentricity * Mathf.Sin(uBeziers * Mathf.PI * 2);

        Position = (1 - uBeziers) * _p0 + uBeziers * _p1;
    }
}