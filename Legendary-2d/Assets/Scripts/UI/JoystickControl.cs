using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickControl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform container;
    private RectTransform core;
    [SerializeField]
    private float speedDrag;

    private Vector2 pos;
    private float radiusCal;
    private bool isDrag;
    //private Vector3 InputDirection;

    void Start()
    {
        container = GetComponent<RectTransform>();
        core = transform.GetChild(0).GetComponent<RectTransform>();
        core.anchoredPosition = Vector3.zero;

        radiusCal = container.sizeDelta.x / 2;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.Instance.Character.directMove = -1;
            //Call event
            EventManager.CallEvent(GameEvent.MOVE_CHARACTER);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.Instance.Character.directMove = 1;
            //Call event
            EventManager.CallEvent(GameEvent.MOVE_CHARACTER);
        }
#endif
        if (isDrag && Time.frameCount % 5 == 0)
            DragAction();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Calculate position in the Joystick 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out pos);
        pos *= Vector2.right;
        //DragAction();
    }

    private void DragAction()
    {
        var posValue = pos.magnitude;
        if (posValue <= radiusCal - 15)
        {
            core.anchoredPosition = pos;
        }
        else
        {
            core.anchoredPosition = pos.normalized * (radiusCal - 15);
        }

        if (GameManager.Instance.Character.timeMove <= 0 && posValue >= radiusCal / 4)
        {
            //Get direct
            if (core.anchoredPosition.x > 0)
                GameManager.Instance.Character.directMove = 1;
            else
                GameManager.Instance.Character.directMove = -1;

            //Call event
            EventManager.CallEvent(GameEvent.MOVE_CHARACTER);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        core.anchoredPosition = Vector3.zero;
    }
}
