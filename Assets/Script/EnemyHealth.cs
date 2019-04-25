using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip destroySound;
    public int enemyHP;
    private Slider silider;

    public int scoreValue;
    private ScoreManager sm;

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
                Destroy(transform.root.gameObject);

                //破壊効果音
                AudioSource.PlayClipAtPoint(destroySound,
                    transform.position);

            //敵を破壊した瞬間にスコアを加算するメソッドを呼び出す
            //引数にはscoreValueを入れる
            sm.AddScore(scoreValue);
            }
        }
    }
}
