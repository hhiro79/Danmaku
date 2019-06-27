using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMissileItem : ItemBase
{
    private int maxSpecialMissile = 5;
    public int addSpecialMissile;

    public override void ItemEffects()
    {
        base.ItemEffects();

        //SP使用回数が加算される
        PlayerHealth.currentSpecialMissile += addSpecialMissile;
        if(PlayerHealth.currentSpecialMissile > maxSpecialMissile)
        {
            PlayerHealth.currentSpecialMissile = maxSpecialMissile;
        }

        //SpecialShotButtonを取得してメソッド実行、ボタン表示
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpecialShotButton>().ActiveSpecialButton();

        base.DestroyItem();
    }
}