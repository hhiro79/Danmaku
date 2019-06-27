using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMissile : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    public float missileSpeed;
    private int timeCount = 0;
    public float stopTimer;

    // Update is called once per frame
    void Update()
    {
        timeCount += 1;

        //タイマーを進める
        stopTimer -= Time.deltaTime;

        //タイマーが0未満になったら0で止める
        if(stopTimer < 0) {
            stopTimer = 0;
        }
        
        //print("攻撃開始まであと" + stopTimer + "秒");

        //タイマーが0以下になったら敵が攻撃を開始する
        if(timeCount % 60 == 0 && stopTimer <= 0) {

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
    /// <summary>
    /// タイマーの時間を増加させる 
    /// </summary>
    /// <param name="amount"></param>
    public void AddStopTimer(float amount) {
            stopTimer += amount;
    }
}
