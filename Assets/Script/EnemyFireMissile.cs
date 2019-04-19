using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMissile : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    public float missileSpeed;
    private int timeCount = 0;

    // Update is called once per frame
    void Update()
    {
        timeCount += 1;
        
        if(timeCount % 60 == 0) {

            //敵ミサイル生成
            GameObject enemyMissile = Instantiate
                (enemyMissilePrefab, transform.position,
                 Quaternion.identity) as GameObject;
            
            Rigidbody enemyMissileRb =
             enemyMissile.GetComponent<Rigidbody>();

            //ミサイルを飛ばす方向を決める。forwardはZ軸方向
            enemyMissileRb.AddForce(transform.forward * missileSpeed);

            //3秒後に敵のミサイルを削除
            Destroy(enemyMissile, 3.0f);
        }
    }
}
