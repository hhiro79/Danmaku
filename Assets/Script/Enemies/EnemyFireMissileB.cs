using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMissileB : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    public float misssileSpeed = -300;
    public float missileWaitTime = 5;
    private int timeCount = 0;

    private Rotate rotate;

    private LookAt lookAt;

    // Update is called once per frame
    void Update()
    {
        timeCount += 1;

        //発射間隔を短く
        if(timeCount % missileWaitTime == 0){
            GameObject enemyMissile = Instantiate (enemyMissilePrefab,
                transform.position, Quaternion.identity) as GameObject;
            Rigidbody enemyMissileRb = enemyMissile.GetComponent<Rigidbody> ();
            enemyMissileRb.AddForce (transform.forward * misssileSpeed);

            //10秒後に敵のミサイルを削除
            Destroy(enemyMissile, 10.0f);

            //timeCountが500になったとき、このオブジェクトにRotateスクリプトを付加
            if(timeCount == 500){
                if(!lookAt){
                    lookAt = GetComponent<LookAt>();
                    lookAt.enabled = false;
                } else {
                    lookAt.enabled = false;
                }

                if(!rotate){
                    rotate = this.gameObject.AddComponent<Rotate> ();
                } else {
                    rotate.enabled = true;
                }
                rotate.pos = new Vector3(0, 90, 0);
            }

            //timeCountが1000になったとき、戻す
            if(timeCount >= 1000){
                rotate.enabled = false;
                lookAt.enabled = true;
                timeCount = 0;
            }
        }        
    }
}
