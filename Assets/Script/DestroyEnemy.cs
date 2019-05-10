using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    //Is Triggerにチェック
    //EnemyCにEnemyタグを付ける

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Enemy") {
            Destroy (col.gameObject);
        }
    }
}
