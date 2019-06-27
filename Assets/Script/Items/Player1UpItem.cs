using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1UpItem : ItemBase
{
    private PlayerHealth playerHealth;
    public int reward = 1;

    public override void SetUp()
    {
        //PlayerについているPlayerHealthスクリプトにアクセス
        playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth>();
    }

    public override void ItemEffects()
    {
        base.ItemEffects();

        //プレーヤーの残機を回復
        playerHealth.Player1Up(reward);

        base.DestroyItem();
    }
}
