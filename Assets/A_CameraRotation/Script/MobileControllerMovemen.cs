using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileControllerMovemen : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    public RectTransform touchArea;
    public bool invertXAxis = false;
    public bool invertYAxis = false;
    [Range(0, 2)] public float handleLimit = 1f;

    private Vector2 inputVector;
    private Vector2 joystickStartPosition;

    private void Start()
    {
        joystickBackground.gameObject.SetActive(false);
        joystickHandle.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPointerWithinTouchArea(eventData))
        {
            Vector2 touchPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea, eventData.position, eventData.pressEventCamera, out touchPos);

            joystickStartPosition = touchPos;

            joystickBackground.anchoredPosition = touchPos;
            joystickHandle.anchoredPosition = Vector2.zero;

            joystickBackground.gameObject.SetActive(true);
            joystickHandle.gameObject.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (joystickBackground.gameObject.activeSelf)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea, eventData.position, eventData.pressEventCamera, out pos);

            Vector2 offset = pos - joystickStartPosition;
            Vector2 clampedOffset = Vector2.ClampMagnitude(offset, joystickBackground.sizeDelta.x / 2 * handleLimit);

            joystickHandle.anchoredPosition = clampedOffset;
            inputVector = clampedOffset / (joystickBackground.sizeDelta.x / 2 * handleLimit);

            if (invertXAxis) inputVector.x = -inputVector.x;
            if (invertYAxis) inputVector.y = -inputVector.y;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickBackground.gameObject.SetActive(false);
        joystickHandle.gameObject.SetActive(false);
        inputVector = Vector2.zero;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }

    private bool IsPointerWithinTouchArea(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea, eventData.position, eventData.pressEventCamera, out localPoint);
        return touchArea.rect.Contains(localPoint);
    }
}
