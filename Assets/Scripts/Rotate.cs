using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector2 _speed = new(50, 50);
    [SerializeField] private int _yMinLimit = -80;
    [SerializeField] private int _yMaxLimit = 80;
    [SerializeField] private float _rotationDamping = 5.0f;

    private float _xDeg = 0.0f;
    private float _yDeg = 0.0f;
    private Quaternion _currentRotation;
    private Quaternion _desiredRotation;
    private Quaternion _rotation;

    private float YDeg
    {
        get => _yDeg;
        set => _yDeg = ExtraMath.ClampAngle(value, _yMinLimit, _yMaxLimit);
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _rotation = transform.rotation;
        _currentRotation = transform.rotation;
        _desiredRotation = transform.rotation;

        _xDeg = Vector3.Angle(Vector3.right, transform.right);
        YDeg = Vector3.Angle(Vector3.up, transform.up);
    }

    private void LateUpdate()
    {
        if (IsSingleTouch() && IsSwipe())
        {
            Vector2 touchposition = Input.GetTouch(0).deltaPosition;

            _xDeg -= touchposition.x * _speed.x * 0.002f;
            YDeg -= touchposition.y * _speed.y * 0.002f;
        }

        Transform figure = transform.GetChild(0).GetChild(0);

        _desiredRotation = Quaternion.Euler(YDeg, _xDeg, 0);
        _currentRotation = figure.rotation;
        _rotation = Quaternion.Lerp(_currentRotation, _desiredRotation, Time.deltaTime * _rotationDamping);

        figure.rotation = _rotation;
    }

    private static bool IsSingleTouch() => Input.touchCount == 1;
    private static bool IsSwipe() => Input.GetTouch(0).phase == TouchPhase.Moved;
}
