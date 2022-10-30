using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector2 _speed = new(20, 20);

    private void LateUpdate()
    {
        if (IsSingleTouch() && IsSwipe())
        {
            Vector2 touchposition = Input.GetTouch(0).deltaPosition;

            float x = -touchposition.x * _speed.x * Time.deltaTime;
            float y = -touchposition.y * _speed.y * Time.deltaTime;

            Transform figure = transform.GetChild(0).GetChild(0);
            figure.Rotate(y, x, 0);
        }
    }

    private static bool IsSingleTouch() => Input.touchCount == 1;
    private static bool IsSwipe() => Input.GetTouch(0).phase == TouchPhase.Moved;
}
