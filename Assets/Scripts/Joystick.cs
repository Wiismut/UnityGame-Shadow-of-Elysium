using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;

    public Vector2 InputDirection { get; private set; }

    private void Start()
    {
        if (joystickBackground == null)
            joystickBackground = GetComponent<RectTransform>();

        if (joystickHandle == null)
            Debug.LogWarning("джойстик не прикреплен");

        InputDirection = Vector2.zero;
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBackground.sizeDelta.x) * 2;
            pos.y = (pos.y / joystickBackground.sizeDelta.y) * 2;

            Vector2 rawDirection = new Vector2(pos.x, pos.y);

            if (rawDirection.magnitude > 1)
                rawDirection.Normalize();

            InputDirection = GetLockedDirection(rawDirection);

            if (joystickHandle != null)
            {
                joystickHandle.anchoredPosition = new Vector2(
                    InputDirection.x * (joystickBackground.sizeDelta.x / 2),
                    InputDirection.y * (joystickBackground.sizeDelta.y / 2));
            }
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector2.zero;
        if (joystickHandle != null)
            joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal => InputDirection.x;
    public float Vertical => InputDirection.y;

    private Vector2 GetLockedDirection(Vector2 rawDirection)
    {
        if (rawDirection == Vector2.zero)
            return Vector2.zero;
        float angle = Mathf.Atan2(rawDirection.y, rawDirection.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;
          if (angle >= 67.5f && angle < 112.5f)
            return Vector2.up;
        if (angle >= 157.5f && angle < 202.5f)
            return Vector2.left;
        if (angle >= 247.5f && angle < 292.5f)
            return Vector2.down;
        if ((angle >= 337.5f && angle < 360f) || (angle >= 0f && angle < 22.5f))
            return Vector2.right;
        if (angle >= 22.5f && angle < 67.5f)
            return (Vector2.up + Vector2.right).normalized;
        if (angle >= 112.5f && angle < 157.5f)
            return (Vector2.up + Vector2.left).normalized;
        if (angle >= 202.5f && angle < 247.5f)
            return (Vector2.down + Vector2.left).normalized;
        if (angle >= 292.5f && angle < 337.5f)
            return (Vector2.down + Vector2.right).normalized;

        return Vector2.zero;
    }
}
