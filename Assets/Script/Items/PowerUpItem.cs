using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip getSound;
    private GameObject fireMissilePod1;
    private GameObject fireMissilePod2;

    // Start is called before the first frame update
    void Start()
    {
        fireMissilePod1 = GameObject.Find ("FireMissileB");
        fireMissilePod2 = GameObject.Find ("FireMissileC");
    }

    void OnTriggerEnter (Collider col){
        if (col.gameObject.CompareTag("Missile")){

            //エフェクト発生
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            //効果音
            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position, 0.4f);

            //アイテムを非アクティブに
            //ここでアイテムを破壊するとメモリ上から消えてNomalメソッドが実行されなくなる
            this.gameObject.SetActive(false);

            //FireMissileスクリプトを有効に
            fireMissilePod1.GetComponent<FireMissile>().enabled = true;
            fireMissilePod2.GetComponent<FireMissile>().enabled = true;

            Invoke("Normal", 3);

            Destroy (effect, 2.0f);
        }
    }

    //プレイヤーの攻撃力を戻す
    void Normal(){

        //FireMissileスクリプトを無効に
        fireMissilePod1.GetComponent<FireMissile> ().enabled = false;
        fireMissilePod2.GetComponent<FireMissile> ().enabled = false;

        //アイテムをメモリ上から消す
        Destroy (this.gameObject);
    }
}
