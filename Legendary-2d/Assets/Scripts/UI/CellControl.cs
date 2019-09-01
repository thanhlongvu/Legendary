using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Color enterColor;
    private Color exitColor;
    private Image img;

    private RectTransform rect;
    private RectTransform CellPanel;

    public Vector2 Coordinate { get; set; }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        exitColor = img.color;
    }

    private void Start()
    {
        CellPanel = GetComponentInParent<RectTransform>();
    }

    private void OnEnable()
    {
        img.color = exitColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.color = enterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.color = exitColor;
    }

    public Vector2 ConvertToWorldPos()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CellPanel, rect.anchoredPosition, null, out Vector2 pos);
        return rect.position;
    }
}
