
using UnityEngine;

public class LockPin : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float minY = -150f;
    [SerializeField] private float maxY = 150f;
    [SerializeField] private float speed = 180f;

    private RectTransform pin;
    private Vector2 initialPosition;
    private int direction = 1;
    private bool isMoving;
    private bool isLocked;

    void Awake()
    {
        pin = GetComponent<RectTransform>();
        initialPosition = pin.anchoredPosition;
    }

    void Update()
    {
        if (!isMoving || isLocked)
            return;

        Vector2 position = pin.anchoredPosition;

        position.y += direction * speed * Time.unscaledDeltaTime;

        if (position.y >= maxY)
        {
            position.y = maxY;
            direction = -1;
        }
        else if (position.y <= minY)
        {
            position.y = minY;
            direction = 1;
        }

        pin.anchoredPosition = position;
    }

    public void SetMoving(bool value)
    {
        isMoving = value;
    }

    public void SetLocked()
    {
        isLocked = true;
        isMoving = false;
    }

    public void ResetPin()
    {
        pin.anchoredPosition = initialPosition;
        direction = 1;
        isLocked = false;
        isMoving = false;
    }

    public bool IsInsideZone(RectTransform zone)
    {
        Vector3[] corners = new Vector3[4];
        zone.GetWorldCorners(corners);

        float minZoneY = corners[0].y;
        float maxZoneY = corners[1].y;

        float pinY = pin.position.y;

        return pinY >= minZoneY && pinY <= maxZoneY;
    }
}
