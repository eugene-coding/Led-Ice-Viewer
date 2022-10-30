using UnityEngine;
using UnityEngine.Rendering;

public class Zoom : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Zoom")]
    [SerializeField] private float _zoomMinClamp;
    [SerializeField] private float _zoomMaxClamp;
    [SerializeField] private float _zoomSmooth;

    [Header("Position")]
    [SerializeField] private float _positionMinClamp;
    [SerializeField] private float _positionMaxClamp;
    [SerializeField] private float _positionSmooth;

    private float _distance;
    private float _deltaDistance;
    private float _zoom;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        if (Input.touchCount == 2)
        {
            var first = Input.GetTouch(0);
            var second = Input.GetTouch(1);

            float currentDistance = Vector3.Distance(first.position, second.position);

            _deltaDistance = _distance - currentDistance;

            _distance = Vector3.Distance(first.position, second.position);

            _zoom = Mathf.Clamp(_deltaDistance, _zoomMinClamp, _zoomMaxClamp) * _speed * Time.deltaTime;
        }

        UpdateZoom();
        UpdatePosition();

        Vector3 pos = _camera.transform.position;
        pos.x = Mathf.Clamp(pos.x, _positionMinClamp, _positionMaxClamp);
        _camera.transform.position = pos;
    }

    private void UpdateZoom()
    {
        _zoom = Mathf.Lerp(_zoom, 0, _zoomSmooth * Time.deltaTime);
    }

    private void UpdatePosition()
    {
        var current = _camera.transform.position;
        var target = _camera.transform.position + Vector3.right * _zoom;

        _camera.transform.position = Vector3.Lerp(current, target, _positionSmooth * Time.deltaTime);
    }
}