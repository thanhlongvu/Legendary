using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickControl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform container;
    private RectTransform core;

    private Vector3 InputDirection;

    void Start()
    {
        container = GetComponent<RectTransform>();
        core = transform.GetChild(0).GetComponent<RectTransform>();
        core.anchoredPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Calculate position in the Joystick 
        Vector2 pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out pos);

        var radiusCal = container.sizeDelta.x / 2;
        pos *= Vector2.right;

        if(pos.magnitude <= radiusCal - 15)
        {
            core.anchoredPosition = pos;
        }
        else
        {
            core.anchoredPosition = pos.normalized * (radiusCal - 15);
        }

        //TODO: setup InputDirection

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        core.anchoredPosition = Vector3.zero;
    }
}
