using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : ItemBase
{
    public GameObject shieldPrefab;

    private GameObject player;
    private Vector3 pos;

    public override void ItemEffects()
    {
        base.ItemEffects();

        //プレーヤーの位置情報取得
        player = GameObject.FindGameObjectWithTag("Player");
        pos = player.transform.position;

        //プレーヤーの前後の位置に2枚の防御シールドを発生
        GameObject shieldA = (GameObject)Instantiate (shieldPrefab, new Vector3(pos.x, pos.y, pos.z - 1.5f), Quaternion.identity);
        GameObject shiledB = (GameObject)Instantiate (shieldPrefab, new Vector3(pos.x, pos.y, pos.z + 1.5f), Quaternion.identity);

        //発生させた防御シールドをプレイヤーの子供に設定
        //親子関係にすることで一緒に動く
        shieldA.transform.SetParent(player.transform);
        shiledB.transform.SetParent(player.transform);

        //防御シールドを8秒後に消滅
        Destroy(shieldA, 8.0f);
        Destroy(shiledB, 8.0f);

        base.DestroyItem();
    }
}
