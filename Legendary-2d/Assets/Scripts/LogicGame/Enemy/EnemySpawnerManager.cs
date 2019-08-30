using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField]
    private RoundGame[] rounds;

    //[SerializeField]
    //private Transform pools;

    private GameObject finalRoundEnemy;

    [SerializeField]
    private GameObject CharacterPrefab;

    void Start()
    {
        StartCoroutine(PlayRounds());
        Invoke("SetDistanceCol", 1f);

        //Instance CHARACTER
        StartCoroutine(InstanceCharacter());
    }

    private IEnumerator InstanceCharacter()
    {
        yield return StaticObjects.WAIT_TIME_ZERO_POINT_ONE;
        Instantiate(CharacterPrefab, transform.GetChild(transform.childCount - 1).position, Quaternion.identity);
    }

    private IEnumerator PlayRounds()
    {
        yield return StaticObjects.WAIT_TIME_THREE;
        for (int i = 0; i < rounds.Length; i++)
        {
            StartRound(i);
            yield return new WaitForSeconds((rounds[i].NumberEnemy - 1) * rounds[i].timeDelta + rounds[i].timeMore);
        }
    }

    private void StartRound(int index)
    {
        StartCoroutine(Spawn(rounds[index].Enemies, rounds[index].NumberEnemy, rounds[index].timeDelta));
    }

    private IEnumerator Spawn(GameObject[] objs, int number, float timeDelta)
    {
        EventManager.CallEvent(GameEvent.ROUND_DONE);

        yield return StaticObjects.WAIT_TIME_FOUR;
        for (int i = 0; i < number; i++)
        {
            //Random enemy
            int posIndex = Random.Range(0, 1000) / 200;
            int enemyIndex = Random.Range(0, objs.Length);
            //var enemy = Instantiate(objs[enemyIndex], pools) as GameObject;
            var enemy = PoolManager.Instance.PopPool(PoolName.BASE_ENEMY.ToString(), transform.GetChild(posIndex).position, Quaternion.identity);
            if (i == number - 1)
                finalRoundEnemy = enemy;

            //waiting
            yield return new WaitForSeconds(timeDelta);
        }
    }

    private bool AllEnemyDie()
    {
        if (finalRoundEnemy != null)
        {
            return false;
        }
        return true;
    }

    private void SetDistanceCol()
    {
        GameManager.Instance.DISTANCE_COL = Vector3.Distance(transform.GetChild(0).position, transform.GetChild(1).position);
    }
}

[System.Serializable]
public struct RoundGame
{
    public GameObject[] Enemies;
    public int NumberEnemy;
    public float timeDelta;
    public int timeMore;
}

//[System.Serializable]
//public struct EnemyTurn
//{
//    public GameObject EnymyPrefab;

//    [Range(0, 1)]
//    public float Ratio;
//}