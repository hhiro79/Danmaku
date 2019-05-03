using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip getSound;
    public GameObject shieldPrefab;

    private GameObject player;
    private Vector3 pos;

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Missile")) {

            //エフェクトとSEを発生
            GameObject effect = Instantiate (effectPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position, 0.4f);

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

            //防御シールドを5秒後に消滅
            Destroy(shieldA, 5.0f);
            Destroy(shiledB, 5.0f);
            
            //アイテムを破壊
            Destroy(gameObject);
            Destroy(effect, 2.0f);
        }
    }
}
