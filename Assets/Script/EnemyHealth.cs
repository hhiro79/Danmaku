using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip destroySound;
    public int enemyHP;
    private Slider silider;

    public int scoreValue;
    private ScoreManager sm;

    //アイテム出現
    public GameObject[] itemPrefab;

    //ステージクリア
    public int nextSceneNumber;
    public AudioClip clearSound;

    void Start(){
        //GameObject.Find("oo")の使い方。
        //名前でオブジェクト指定できる
        silider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();

        //スライダーの最大値の設定
        silider.maxValue = enemyHP;

        //スライダーの現在値
        silider.value = enemyHP;

        //スコア
        //ScoreLabelオブジェクトについているScoreManagerスクリプトにアクセス
        sm = GameObject.Find("ScoreLabel").GetComponent<ScoreManager>();
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
            enemyHP -= 1;

            //この1行がないとスライダーバーの目盛りが変化しない
            silider.value = enemyHP;

            //ミサイル削除
            Destroy(col.gameObject);

            //敵のHPが0になったら敵オブジェクトを破壊
            if(enemyHP == 0){

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

                //ステージクリア
                //親オブジェクトにBossというTagがついていたならば
                if(this.gameObject.transform.root.CompareTag("Boss")){

                    //クリア音を鳴らす
                    AudioSource.PlayClipAtPoint(clearSound, Camera.main.transform.position, 0.4f);

                    //1秒後にシーン遷移のメソッドを実行
                    Invoke("GoNextStage", 1);
                }
            }
        }
    }

    //ステージクリア
    //シーン遷移メソッド
    void GoNextStage(){
        SceneManager.LoadScene(nextSceneNumber);
    }
}
