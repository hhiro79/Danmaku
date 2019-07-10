using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAttackItem : ItemBase
{
    public GameObject[] targets;
    public GameObject[] targetsB;

    public override void Update()
    {
        base.Update();

        //EnemyFireMissileプレハブにタグを付けること
        targets = GameObject.FindGameObjectsWithTag("EnemyFireMissile");
        targetsB = GameObject.FindGameObjectsWithTag("EnemyFireMissileB");

        //Debug.Log(targets.Length);
    }

    public override void ItemEffects()
    {
        StopMissile();
        base.DestroyItem();
    }

    public void StopMissile()
    {
        if(targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<EnemyFireMissile>().AddStopTimer(10.0f);
            }
        }
        if (targetsB.Length > 0)
        {
            for (int i = 0; i < targetsB.Length; i++)
            {
                targetsB[i].GetComponent<EnemyFireMissileB>().AddStopTimer(10.0f);
            }
        }
    }
}
