using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : ItemBase
{
    private FireMissile fireMissilePod1;
    private FireMissile fireMissilePod2;
    private FireMissile mainPod;
    public float powerUpCount = 15.0f;

    public override void SetUp()
    {
        fireMissilePod1 = GameObject.Find ("FireMissileB").GetComponent<FireMissile>();
        fireMissilePod2 = GameObject.Find ("FireMissileC").GetComponent<FireMissile>();
        mainPod = GameObject.Find("FireMissile").GetComponent<FireMissile>();
    }

    public override void Update()
    {
        base.Update();

        if (fireMissilePod1.subPod)
        {
            fireMissilePod1.shotPower = mainPod.shotPower;
        }
        if (fireMissilePod2.subPod)
        {
            fireMissilePod2.shotPower = mainPod.shotPower;
        }
    }

    public override void ItemEffects()
    {
        base.ItemEffects();

        //アイテムを非アクティブに
        //ここでアイテムを破壊するとメモリ上から消えてNomalメソッドが実行されなくなる
        this.gameObject.SetActive(false);

        //FireMissileスクリプトのサブポッドフラグを有効に
        fireMissilePod1.subPod = true;
        fireMissilePod2.subPod = true;

        Invoke("Normal", powerUpCount);

        //エフェクトを消す
        Destroy(effect, 2.0f);
    }

    //プレイヤーの攻撃力を戻す
    void Normal(){

        //FireMissileスクリプトのサブポッドフラグを無効に
        fireMissilePod1.subPod = false;
        fireMissilePod2.subPod = false;

        //アイテムをメモリ上から消す
        Destroy (this.gameObject);
    }
}
