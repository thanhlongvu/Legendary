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
        StartCoroutine(SortSpawner());
    }

    private IEnumerator SortSpawner()
    {
        yield return StaticObjects.WAIT_TIME_QUARTER;
        var posChar = cam.ScreenToWorldPoint(characterSpawnerUI.position);
        characterSpawner.position = new Vector2(posChar.x, posChar.y);

        for (int i = 0; i < colls.Length; i++)
        {
            var pos = cam.ScreenToWorldPoint(collsUI[i].position);
            colls[i].position = new Vector2(pos.x, pos.y);
        }
    }
}
