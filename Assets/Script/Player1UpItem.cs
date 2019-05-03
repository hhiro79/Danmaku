using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1UpItem : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip getSound;
    private PlayerHealth playerHealth;
    public int reward = 1;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerについているPlayerHealthスクリプトにアクセス
        playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider col){

        //プレーヤーのミサイルで破壊すると残機が回復
        if(col.gameObject.CompareTag("Missile")){

            //エフェクト発生
            GameObject effect = Instantiate (effectPrefab, transform.position, Quaternion.identity);

            //効果音を出す
            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position, 0.4f);

            //アイテムを破壊
            Destroy(this.gameObject);
            Destroy(effect, 2.0f);

            //プレーヤーの残機を回復
            playerHealth.Player1Up(reward);
        } 
    }
}
