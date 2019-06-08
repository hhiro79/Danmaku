using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip destroySound;
    public int enemyHP;
    private int currentHP;
    private Slider silider;

    public int scoreValue;
    private ScoreManager sm;
    private StageNorma sn;

    //アイテム出現
    public GameObject[] itemPrefab;

    //ステージクリア
    public int nextSceneNumber;
    public AudioClip clearSound;

    public bool isSecretBonusEnemy;

    void Start(){

        currentHP = enemyHP;

        //GameObject.Find("oo")の使い方。
        //名前でオブジェクト指定できる
        //silider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();

        //スライダーの最大値の設定
        //silider.maxValue = enemyHP;

        //スライダーの現在値
        //silider.value = currentHP;

        //スコア
        //ScoreLabelオブジェクトについているScoreManagerスクリプトにアクセス
        sm = GameObject.FindGameObjectWithTag("StageManager").GetComponent<ScoreManager>();

        //AddDestroyCount
        sn = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageNorma>();
    }

    void OnTriggerEnter(Collider col){

        //もしもぶつかった相手にMissileというタグが付いてたら
        if(col.gameObject.CompareTag("Missile")){
            
            //エフェクト発生
            GameObject effect = Instantiate (effectPrefab,
                transform.position, Quaternion.identity)
                as GameObject;
            
            //0.5秒後にエフェクトを消す
            Destroy(effect, 0.5f);

            //敵のHPを減少させる
            currentHP -= 1;
            if(currentHP <= Mathf.FloorToInt(enemyHP/50.0f)){
                EnemyGene2 e2 = GameObject.Find("EnemyGene2").
                    GetComponent<EnemyGene2>();
                    e2.CreateEnemy();
                    if(!e2){
                        return;
                    }
                    Debug.Log("敵生成");
            }

            //この1行がないとスライダーバーの目盛りが変化しない
            //silider.value = currentHP;

            //ミサイル削除
            Destroy(col.gameObject);

            //敵のHPが0になったら敵オブジェクトを破壊
            if(currentHP == 0){

                if (isSecretBonusEnemy)
                {
                    GameObject.FindGameObjectWithTag("StageManager").
                        GetComponent<CheckBonus>().AddSecretEnemyBonus();
                }

                //StageNormaのAddDestroyCount呼び出し
                sn.AddDestroyCount();

                //敵オブジェクト破壊
                //Destroy(transform.root.gameObject);

                //ステージクリア
                //親オブジェクトを非表示に
                transform.root.gameObject.SetActive(false);

                //破壊効果音
                AudioSource.PlayClipAtPoint(destroySound,
                    transform.position);

                //敵を破壊した瞬間にスコアを加算するメソッドを呼び出す
                //引数にはscoreValueを入れる
                sm.AddScore(scoreValue);

                if (itemPrefab.Length > 0) {
                    //ランダムメソッドの活用
                    GameObject dropItem = itemPrefab [Random.Range (0, itemPrefab.Length)];
                    
                    //敵を破壊した瞬間にアイテムプレハブを実体化
                    Instantiate (dropItem, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
