using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField]
    private RoundGame[] rounds;

    [SerializeField]
    private Transform pools;

    private GameObject finalRoundEnemy;
    void Start()
    {
        StartCoroutine(PlayRounds());
    }

    private IEnumerator PlayRounds()
    {
        yield return StaticObjects.WAIT_TIME_THREE;
        for (int i = 0; i < rounds.Length; i++)
        {
            StartRound(i);
            yield return new WaitForSeconds((rounds[i].NumberEnemy - 1) * rounds[i].timeDelta + rounds[i].timeMore);
            LogSystem.LogWarning("End round " + i);
        }
    }

    private void StartRound(int index)
    {
        LogSystem.LogSuccess("Start round " + index);
        StartCoroutine(Spawn(rounds[index].Enemies, rounds[index].NumberEnemy, rounds[index].timeDelta));
    }

    private IEnumerator Spawn(GameObject[] objs, int number, float timeDelta)
    {
        EventManager.CallEvent(GameEvent.ROUND_DONE);

        yield return StaticObjects.WAIT_TIME_FOUR;
        for (int i = 0; i < number; i++)
        {
            //Random enemy
            int enemyIndex = Random.Range(0, objs.Length);
            var enemy = Instantiate(objs[enemyIndex], pools) as GameObject;
            if (i == number - 1)
                finalRoundEnemy = enemy;

            //Set position
            int posIndex = Random.Range(0, 1000) / 200;
            enemy.transform.position = transform.GetChild(posIndex).position;
            enemy.transform.rotation = Quaternion.identity;

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