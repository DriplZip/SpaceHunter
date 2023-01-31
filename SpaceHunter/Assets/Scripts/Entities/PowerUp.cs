using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private Vector2 _rotationMinMax = new Vector2(15, 90);
    [SerializeField] private Vector2 _driftMinMax = new Vector2(0.25f, 2);
    [SerializeField] private float _lifeTime = 6f;
    [SerializeField] private float _fadeTime = 2f;

    [Header("Set dynamically")]
    public WeaponType bonusType;
    public GameObject cube;
    public TextMesh letter;
    public Vector3 rotationPerSecond;
    public float birthTime;

    private Rigidbody _rigidbody;
    private BordersCheck _bordersCheck;
    private Renderer _cubeRenderer;

    private void Awake()
    {
        cube = transform.Find("Cube").gameObject;
        letter = GetComponent<TextMesh>();
        _rigidbody = GetComponent<Rigidbody>();
        _bordersCheck = GetComponent<BordersCheck>();
        _cubeRenderer = cube.GetComponent<Renderer>();

        Vector3 velocity = Random.onUnitSphere;
        velocity.z = 0;
        velocity.Normalize();
        velocity *= Random.Range(_driftMinMax.x, _driftMinMax.y);
        _rigidbody.velocity = velocity;

        transform.rotation = Quaternion.identity;

        rotationPerSecond = new Vector3(Random.Range(_rotationMinMax.x, _rotationMinMax.y),
            Random.Range(_rotationMinMax.x, _rotationMinMax.y), Random.Range(_rotationMinMax.x, _rotationMinMax.y));

        birthTime = Time.time;
    }

    private void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotationPerSecond * Time.time);

        float fading = (Time.time - (birthTime + _lifeTime)) / _fadeTime;

        if (fading >= 1)
        {
            Destroy(this.gameObject);
            
            return;
        }

        if (fading > 0)
        {
            Color color = _cubeRenderer.material.color;
            color.a = 1f - fading;
            _cubeRenderer.material.color = color;
            color = letter.color;
            color.a = 1f - (fading * 0.5f);
            letter.color = color;
        }

        if (!_bordersCheck.IsOnScreen)
        {
            Destroy(gameObject);
        }
    }

    public void SetBonusType(WeaponType type)
    {
        WeaponDefinition definition = EnemySpawner.GetWeaponDefinition(type);
        _cubeRenderer.material.color = definition.ColorBonus;
        letter.text = definition.Letter;
        bonusType = type;
    }

    public void AbsorbedBy(GameObject target)
    {
        Destroy(this.gameObject);
    }
}
