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

    public bool isSecretBonusEnemy;

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
        if (col.gameObject.tag == "Missile" || col.gameObject.tag == "Special")
        {

            bool specialFlag = false;
            if (col.gameObject.CompareTag("Special"))
            {
                specialFlag = true;
            }

            //エフェクト発生
            GameObject effect = Instantiate(effectPrefab,
                transform.position, Quaternion.identity)
                as GameObject;

            //0.5秒後にエフェクトを消す
            Destroy(effect, 0.5f);

            //敵のHPを減少させる
            if (!specialFlag)
            {
                currentHP -= 1;

                //ミサイル削除
                Destroy(col.gameObject);
            }
            else
            {
                currentHP -= 10;
            }

            // 敵のHPが0以下になったら
            if(currentHP <= 0){

                if (isSecretBonusEnemy)
                {
                    GameObject.FindGameObjectWithTag("StageManager").
                        GetComponent<CheckBonus>().AddSecretEnemyBonus();
                }
                
                // 破壊時の効果音再生
                AudioSource.PlayClipAtPoint(destroySound,transform.position);

                // 敵を破壊した瞬間にスコアを加算するメソッドを呼び出す
                // 引数にはscoreValueを入れる
                sm.AddScore(scoreValue);

                //StageNormaのAddDestroyCount呼び出し
                sn.AddDestroyCount();

                if (col.gameObject.tag == "Enemy"){
                    // 敵オブジェクト破壊
                    Destroy(col.gameObject);

                    //敵を破壊した瞬間にアイテムプレハブを実体化
                    if (itemPrefab.Length > 0) {
                        //ランダムメソッドの活用                
                        Instantiate (itemPrefab [Random.Range (0, itemPrefab.Length)], transform.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                if (currentHP <= Mathf.FloorToInt(enemyHP / 50.0f))
                {
                    EnemyGene2 e2 = GameObject.Find("EnemyGene2").
                    GetComponent<EnemyGene2>();
                    e2.CreateEnemy();
                    if (!e2)
                    {
                        return;
                    }
                    Debug.Log("敵生成");
                }
            }
        }
    }
}
    