using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMissileB : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    public float misssileSpeed = -300;
    public float missileWaitTime = 5;
    private int timeCount = 0;
    private float stopTimer;

    private Rotate rotate;
    private LookAt lookAt;
    
    // Update is called once per frame
    void Update()
    {
        if (stopTimer > 0)
        {
            //タイマーを進める
            stopTimer -= Time.deltaTime;

            //タイマーが0未満になったら0で止める
            if (stopTimer < 0)
            {
                stopTimer = 0;
            }
        }

        if (stopTimer == 0)
        {
            timeCount += 1;


            //発射間隔を短く
            if (timeCount % missileWaitTime == 0)
            {
                GameObject enemyMissile = Instantiate(enemyMissilePrefab,
                    transform.position, Quaternion.identity) as GameObject;
                Rigidbody enemyMissileRb = enemyMissile.GetComponent<Rigidbody>();
                enemyMissileRb.AddForce(transform.forward * misssileSpeed);

                //10秒後に敵のミサイルを削除
                Destroy(enemyMissile, 10.0f);

                //timeCountが300になったとき、このオブジェクトにRotateスクリプトを付加
                if (timeCount == 300)
                {
                    if (!lookAt)
                    {
                        lookAt = GetComponent<LookAt>();
                        lookAt.enabled = false;
                    }
                    else
                    {
                        lookAt.enabled = false;
                    }

                    if (!rotate)
                    {
                        rotate = this.gameObject.AddComponent<Rotate>();
                    }
                    else
                    {
                        rotate.enabled = true;
                    }
                    rotate.pos = new Vector3(0, 90, 0);
                }

                //timeCountが600になったとき、戻す
                if (timeCount >= 600)
                {
                    rotate.enabled = false;
                    lookAt.enabled = true;
                    timeCount = 0;
                }
            }
        }        
    }

    /// <summary>
    /// タイマーの時間を増加させる 
    /// </summary>
    /// <param name="amount"></param>
    public void AddStopTimer(float amount)
    {
        stopTimer += amount;
    }
}
