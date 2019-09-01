using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CellManager : MonoBehaviour
{
    [SerializeField]
    private int horizontalNumber;
    [SerializeField]
    private int verticalNumber;
    [SerializeField]
    private RectTransform rootCell;
    [SerializeField]
    private GameObject cellPrefab;

    private float DISTANCE;
    public static int[,] cells;
    void Start()
    {
        StartCoroutine(CreateCell(verticalNumber, horizontalNumber));
        cells = new int[verticalNumber, horizontalNumber];
    }

    private IEnumerator CreateCell(int xNum, int yNum)
    {
        yield return StaticObjects.WAIT_TIME_HAFT;

        DISTANCE = GameManager.Instance.DISTANCE_COL_UI;
        for (int i = 0; i < yNum; i++)
        {
            CreateHorizontal(xNum, rootCell.anchoredPosition + Vector2.up * DISTANCE * i, i);
        }
        gameObject.SetActive(false);
    }

    private void CreateHorizontal(int elementNum, Vector2 root, int yAxis)
    {
        float posX = 0;
        GameObject cellObj = null;
        if(elementNum % 2 == 0)
        {
            for (int i = 0; i < elementNum; i++)
            {
                posX = root.x - (elementNum / 2.0f - 0.5f) * DISTANCE + i * DISTANCE;
                cellObj = Instantiate(cellPrefab, transform) as GameObject;
                cellObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, root.y);
                cellObj.GetComponent<CellControl>().Coordinate = new Vector2(i, yAxis);
            }
        }
        else
        {
            for (int i = 0; i < elementNum; i++)
            {
                posX = root.x - (elementNum / 2) * DISTANCE + i * DISTANCE;
                cellObj = Instantiate(cellPrefab, transform) as GameObject;
                cellObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, root.y);
                cellObj.GetComponent<CellControl>().Coordinate = new Vector2(i, yAxis);
            }
        }
    }

    public static void AttachItem(int x, int y)
    {
        cells[x, y] = 1;
    }

    public static void UnAttackItem(int x, int y)
    {
        cells[x, y] = 0;
    }

    public static bool IsHasItem(int x, int y)
    {
        return cells[x, y] == 1;
    }
}
