using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private float _rotationPerSecond = 0.1f;

    private Material _material;

    [Header("Set Dynamically")] private int _shieldLvlShown;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        int currentShieldLvl = Hero.S.ShieldLvl;

        if (_shieldLvlShown != currentShieldLvl)
        {
            _shieldLvlShown = currentShieldLvl;

            _material.mainTextureOffset = new Vector2(0.2f * _shieldLvlShown, 0);
        }

        float rotationZ = -(_rotationPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}