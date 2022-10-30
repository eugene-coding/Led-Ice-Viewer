using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Transform Target;
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private float _minDistance = .6f;
    [SerializeField] private float _zoomRate = 10.0f;
    [SerializeField] private float _zoomDamping = 5.0f;
    [SerializeField] private float _sensitivity = 0.0025f;

    private float _currentDistance;
    private float _desiredDistance;
    private Vector3 _position;

    private float DesiredDistance
    {
        get => _desiredDistance;
        set => _desiredDistance = Mathf.Clamp(value, _minDistance, _maxDistance);
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        var distance = Vector3.Distance(transform.position, Target.position);

        _currentDistance = distance;
        DesiredDistance = distance;

        _position = transform.position;
    }

    void LateUpdate()
    {
        if (Input.touchCount == 2)
        {
            var firstTouch = Input.GetTouch(0);
            var secondTouch = Input.GetTouch(1);

            var firstTouchPreviousPosition = firstTouch.position - firstTouch.deltaPosition;
            var secondTouchPreviousPosition = secondTouch.position - secondTouch.deltaPosition;

            var currentMagnitude = (firstTouch.position - secondTouch.position).magnitude;
            var previousMagnitude = (firstTouchPreviousPosition - secondTouchPreviousPosition).magnitude;

            var deltaMagnitude = previousMagnitude - currentMagnitude;

            DesiredDistance += deltaMagnitude * Time.deltaTime * _zoomRate * _sensitivity * Mathf.Abs(DesiredDistance);
        }

        _currentDistance = Mathf.Lerp(_currentDistance, DesiredDistance, Time.deltaTime * _zoomDamping);

        _position = Target.position - (Vector3.forward * _currentDistance);

        Vector3 s = new(transform.position.x, transform.position.y, _position.z);

        transform.position = s;
    }
}
