using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<Transform> firstWayPoints;
    [SerializeField] private List<Transform> secondWayPoints;
    [SerializeField] private List<Transform> finalWayPoints;
    [SerializeField] private GameObject[] enemyArray;
    [SerializeField] private int maxEnemies;
    [SerializeField] int enemiesAmmount;
    [SerializeField] List<GameObject> enemiesTipeOneList;
    [SerializeField] List<GameObject> enemiesTipeTwoList;

    private Transform setPoint;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        EnemiesPool(enemyArray[0], enemiesTipeOneList);
        EnemiesPool(enemyArray[1], enemiesTipeTwoList);
        spawnCoroutine = StartCoroutine(ShowEnemy());
    }

    private void EnemiesPool(GameObject enemyType, List<GameObject> enemyList)
    {
        for (int i = 0; i < enemiesAmmount; i++)
        {
            GameObject enemy = Instantiate(enemyType, this.transform);
            enemy.SetActive(false);
            enemyList.Add(enemy);
        }
    }

    public Transform SetWayPoint (int point)
    {

       int index;
        switch (point)
        {
            case 0:
                index = Random.Range(0, firstWayPoints.Count);
                setPoint = firstWayPoints[index];
                break;
            case 1:
                index = Random.Range(0, finalWayPoints.Count);
                setPoint = finalWayPoints [index];
                break;
            case 2:
                index = Random.Range(0, secondWayPoints.Count);
                setPoint = secondWayPoints [index];
                break;
        }

        return setPoint;
    }


    IEnumerator ShowEnemy()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            float probability = Random.value;
            float time = Random.Range(0.3f, 1.5f);

            if (probability < 0.6f)
            {
                InstantiateEnemy(enemiesTipeOneList);
            }
            else
            {
                InstantiateEnemy(enemiesTipeTwoList);   
            }

            yield return new WaitForSeconds(time);
        }

        ShowBoss();
    }

    private GameObject InstantiateEnemy(List<GameObject> enemy)
    {
        for (int i = 0; i < enemy.Count; i++)
        {
            if (!enemy[i].activeInHierarchy)
            {

                enemy[i].SetActive(true);
                return enemy[i];
            }

        }
        return null;
    }

    public void PlayerIsdead()
    {
        StopCoroutine(spawnCoroutine);
    }

    private void ShowBoss()
    {
        Instantiate(enemyArray[2]);
    }
}
