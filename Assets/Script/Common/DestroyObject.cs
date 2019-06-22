using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //Is Triggerにチェック
    //EnemyCにEnemyタグを付ける

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Item") {
            Destroy (col.gameObject);
        }
    }
}
