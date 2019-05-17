
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGene2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int geneAmount;

    [Range(0, 15f)]
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (GeneEnemy());
    }

    public IEnumerator GeneEnemy()
    {
        for (int i = 0; i < geneAmount; i++)
        {
            GameObject enemy = (GameObject) Instantiate(enemyPrefab,
                transform.position, Quaternion.Euler(0, 180, 0));
            //Destroy(enemy, 5.0f);
            yield return new WaitForSeconds (waitTime);
        }
    }
    public void CreateEnemy(){
        StartCoroutine (GeneEnemy());
    }

}
