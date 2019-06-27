using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureItem : ItemBase
{
    private PlayerHealth playerHealth;
    public int reward = 3;

    public override void SetUp()
    {
        //PlayerについているPlayerHealthスクリプトにアクセス
        playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth>();
    }

    public override void ItemEffects()
    {
        base.ItemEffects();

        //プレーヤーのHPをreward分回復
        playerHealth.AddHP(reward);

        base.DestroyItem();
    }
}