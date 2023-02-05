using UnityEngine;

public class BordersCheck : MonoBehaviour
{
    [Header("Set in Inspector")] 
    [SerializeField] private float _repulsionRadius = 3f;
    [SerializeField] private bool _keepOnScreen = true;

    public bool OffRight { get; private set; }
    public bool OffLeft { get; private set; }
    public bool OffUp { get; private set; }
    public bool OffDown { get; private set; }

    public float RepulsionRadius => _repulsionRadius;

    public float CamHeight { get; private set; }

    public float CamWight { get; private set; }

    [field: Header("Set Dynamically")] public bool IsOnScreen { get; private set; } = true;

    private void Awake()
    {
        Camera mainCamera = Camera.main;

        CamHeight = mainCamera.orthographicSize;
        CamWight = CamHeight * mainCamera.aspect;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        IsOnScreen = true;
        OffDown = OffLeft = OffRight = OffUp = false;

        if (position.x > CamWight - _repulsionRadius)
        {
            position.x = CamWight - _repulsionRadius;
            OffRight = true;
        }

        if (position.x < -CamWight + _repulsionRadius)
        {
            position.x = -CamWight + _repulsionRadius;
            OffLeft = true;
        }

        if (position.y > CamHeight - _repulsionRadius)
        {
            position.y = CamHeight - _repulsionRadius;
            OffUp = true;
        }

        if (position.y < -CamHeight + _repulsionRadius)
        {
            position.y = -CamHeight + _repulsionRadius;
            OffDown = true;
        }

        IsOnScreen = !(OffDown || OffLeft || OffRight || OffUp);

        if (_keepOnScreen && !IsOnScreen)
        {
            transform.position = position;
            IsOnScreen = true;

            OffDown = OffLeft = OffRight = OffUp = false;
        }
    }
}