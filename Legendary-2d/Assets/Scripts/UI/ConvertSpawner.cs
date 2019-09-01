using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ConvertSpawner : MonoBehaviour
{
    [Header("In game")]
    public Transform characterSpawner;
    public Transform[] colls;

    [Header("UI")]
    public RectTransform characterSpawnerUI;
    public RectTransform[] collsUI;

    public Camera cam;

    void Start()
    {
        CharacterSpawnPos();
        StartCoroutine(SortSpawner());
    }

    private void CharacterSpawnPos()
    {
        var posChar = cam.ScreenToWorldPoint(characterSpawnerUI.position);
        characterSpawner.position = new Vector2(posChar.x, posChar.y);
    }

    private IEnumerator SortSpawner()
    {
        yield return StaticObjects.WAIT_TIME_QUARTER;
        for (int i = 0; i < colls.Length; i++)
        {
            var pos = cam.ScreenToWorldPoint(collsUI[i].position);
            colls[i].position = new Vector2(pos.x, pos.y);
        }

        //Set col
        GameManager.Instance.MIN_X = colls[0].position.x;
        GameManager.Instance.MAX_X = colls[colls.Length - 1].position.x;

        //Set distance coll UI
        GameManager.Instance.DISTANCE_COL_UI = Vector2.Distance(collsUI[0].anchoredPosition, collsUI[1].anchoredPosition);
    }
}
