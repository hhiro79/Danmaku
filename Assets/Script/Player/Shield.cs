using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //ぶつかってくる敵のミサイルを破壊
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("EnemyMissile")) {
            Destroy (col.gameObject);
        }
    }
}
