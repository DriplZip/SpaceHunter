using UnityEngine;

public class Enemy_3 : Enemy
{
    [Header("Set in Inspector: Enemy_3")] [SerializeField]
    private float _lifeTime = 5;

    private float _birthTime;

    [Header("Set Dynamically: Enemy_3")] private Vector3[] _points;

    private void Start()
    {
        _points = new Vector3[3];
        _points[0] = Position;

        float xMin = -_bordersCheck.CamWight + _bordersCheck.RepulsionRadius;
        float xMax = _bordersCheck.CamWight - _bordersCheck.RepulsionRadius;

        Vector3 point = Vector3.zero;
        point.x = Random.Range(xMin, xMax);
        point.y = -_bordersCheck.CamHeight * Random.Range(2, 2.75f);

        _points[1] = point;

        point = Vector3.zero;
        point.y = Position.y;
        point.x = Random.Range(xMin, xMax);

        _points[2] = point;

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

        Vector3 p01, p12;
        // uBeziers -= 0.2f * Mathf.Sin(uBeziers * Mathf.PI * 2); // Beziers smoothing

        p01 = (1 - uBeziers) * _points[0] + uBeziers * _points[1];
        p12 = (1 - uBeziers) * _points[1] + uBeziers * _points[2];

        Position = (1 - uBeziers) * p01 + uBeziers * p12;
    }
}