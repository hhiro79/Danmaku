using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAttackItem : MonoBehaviour
{
    public GameObject[] targets;

    // Update is called once per frame
    void Update()
    {
        //EnemyFireMissileプレハブにタグを付けること
        targets = GameObject.FindGameObjectsWithTag("EnemyFireMissile");
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Missile") {
            Destroy(col.gameObject);
            StopMissile();
        }
        
        Destroy(gameObject, 1.0f);
    }

    public void StopMissile(){
        for (int i = 0; i < targets.Length; i++) {
            targets [i].GetComponent<EnemyFireMissile> ()
                .AddStopTimer (10.0f);
        }
    }
}
