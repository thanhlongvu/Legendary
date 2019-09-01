using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject itemPrefabUI;
    [SerializeField]
    private Sprite imageItem;
    [SerializeField]
    private RectTransform ItemCanvas;
    [SerializeField]
    private GameObject cellContainer;

    private GameObject itemSpawn;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //show cells
        cellContainer.SetActive(true);
        //Instantiate a prefab
        itemSpawn = Instantiate(itemPrefabUI, ItemCanvas) as GameObject;
        itemSpawn.GetComponent<Image>().sprite = imageItem;

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(ItemCanvas, eventData.position, eventData.pressEventCamera, out Vector2 anchorPos);
        itemSpawn.GetComponent<RectTransform>().anchoredPosition = anchorPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Create item to game enviroment
        RectTransformUtility.ScreenPointToWorldPointInRectangle(ItemCanvas, eventData.position, eventData.pressEventCamera, out Vector3 pos);
        if (FindPosValid(eventData, out Vector2 posObj))
        {
            Instantiate(itemPrefab, posObj, Quaternion.identity);
        }

        //Destroy itemSpawn Or push to pool
        Destroy(itemSpawn);
        //Hide cells
        cellContainer.SetActive(false);
    }

    private bool FindPosValid(PointerEventData eventData, out Vector2 posCenter)
    {
        posCenter = Vector2.zero;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count <= 0)
            return false;
        else
        {
            if (!results[0].gameObject.tag.Equals(GameTag.CELL))
                return false;
            var screenPos = results[0].gameObject.GetComponent<CellControl>().ConvertToWorldPos();
            var coord = results[0].gameObject.GetComponent<CellControl>().Coordinate;
            if (CellManager.IsHasItem((int)coord.x, (int)coord.y))
                return false;
            else
                CellManager.AttachItem((int)coord.x, (int)coord.y);

            posCenter = Camera.main.ScreenToWorldPoint(screenPos);
            return true;
        }
    }
}
