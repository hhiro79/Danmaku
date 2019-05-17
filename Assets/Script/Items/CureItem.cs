using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureItem : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip getSound;
    
    private PlayerHealth playerHealth;
    public int reward = 3;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerについているPlayerHealthスクリプトにアクセス
        playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider col){
        //プレイヤーのミサイルで破壊するとHP回復
        if(col.gameObject.CompareTag ("Missile")) {

            //エフェクト発生
            GameObject effect = Instantiate (effectPrefab, transform.position, Quaternion.identity);

            //効果音出す
            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position, 0.4f);

            //アイテムを画面から消す
            Destroy(this.gameObject);
            Destroy(effect, 2.0f);

            //プレーヤーのHPをreward分回復
            playerHealth.AddHP(reward);
        }
    }
}