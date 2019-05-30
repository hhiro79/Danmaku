using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField,Header("敵破壊時のエフェクト")]
    public GameObject effectPrefab;
    [SerializeField,Header("敵破壊時の効果音")]
    public AudioClip destroySound;
    [SerializeField,Header("敵のHP")]
    public int enemyHP;
    [SerializeField,Header("得点")]
    public int scoreValue;
    [SerializeField,Header("ドロップアイテム群")]
    public GameObject[] itemPrefab;

    public StageNorma sn;
    
    public int currentHP; 
    public ScoreManager sm;
        
    private void Start(){
    	// 現在HPを最大HPに設定
        currentHP = enemyHP;
        
        //スコア
        //ScoreLabelオブジェクトについているScoreManagerスクリプトにアクセス
        sm = GameObject.FindGameObjectWithTag("StageManager").GetComponent<ScoreManager>();
        
        //AddDestroyCount
        sn = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageNorma>();

        Setup();
    }    

    public virtual void Setup(){

    }
    
    public virtual void OnTriggerEnter(Collider col){
        //もしもぶつかった相手にMissileというタグが付いてたら
        if(col.gameObject.CompareTag("Missile")){
        	// ミサイル削除
            Destroy(col.gameObject);
            
            // 破壊時のエフェクト発生
            GameObject effect = Instantiate (effectPrefab, transform.position, Quaternion.identity) as GameObject;
            
            // 0.5秒後にエフェクトを消す
            Destroy(effect, 0.5f);

            // 敵のHPを減少させる
            currentHP -= 1;

            // 敵のHPが0になったら
            if(currentHP == 0){
                // 敵オブジェクト破壊
                Destroy(col.gameObject);

				// 破壊時の効果音再生
                AudioSource.PlayClipAtPoint(destroySound,transform.position);

                // 敵を破壊した瞬間にスコアを加算するメソッドを呼び出す
                // 引数にはscoreValueを入れる
                sm.AddScore(scoreValue);

				//敵を破壊した瞬間にアイテムプレハブを実体化
                if (itemPrefab.Length > 0) {
                    //ランダムメソッドの活用                
                    Instantiate (itemPrefab [Random.Range (0, itemPrefab.Length)], transform.position, Quaternion.identity);
                }                
            }
        }
    }
}
    