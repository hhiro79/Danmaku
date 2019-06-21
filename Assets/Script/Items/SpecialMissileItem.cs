using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMissileItem : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip getSound;

    private int maxSpecialMissile = 5;
    public int addSpecialMissile;

    void OnTriggerEnter(Collider col)
    {
        //このアイテムを破壊したらプレイヤーのSpecialが解放される
        //使用回数が加算される
        if (col.gameObject.CompareTag("Missile"))
        {

            //エフェクト発生
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            //効果音出す
            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position, 0.4f);

            //SP使用回数が加算される
            PlayerHealth.currentSpecialMissile += addSpecialMissile;
            if(PlayerHealth.currentSpecialMissile > maxSpecialMissile)
            {
                PlayerHealth.currentSpecialMissile = maxSpecialMissile;
            }

            //SpecialShotButtonを取得してメソッド実行、ボタン表示
            GameObject.FindGameObjectWithTag("Player").
                GetComponent<SpecialShotButton>().ActiveSpecialButton();

            //アイテムを画面から消す
            Destroy(this.gameObject);
            Destroy(effect, 2.0f);
        }
    }

}
