using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : EnemyBase {

    // ボスのHPバー
    private Slider slider;

    private Text stageNumberText;

    [SerializeField]
    public Slider enemyHPSlider;

    //ステージクリア時の効果音
    public AudioClip clearSound;

    public override void Setup() {
        slider = Instantiate(enemyHPSlider, enemyHPSlider.
            transform.position, Quaternion.identity);
        slider.transform.SetParent(this.gameObject.transform, true);

        //スライダーの最大値の設定
        slider.maxValue = enemyHP;

        //スライダーの現在値
        slider.value = currentHP;
    }

    public override void OnTriggerEnter(Collider col) {
        // 親のOnTriggeEnterを処理する
        base.OnTriggerEnter(col);

        //この1行がないとスライダーバーの目盛りが変化しない
        slider.value = currentHP;

        if(currentHP <= 0){
            //親オブジェクトにBossというTagがついていたならばステージクリア
            if (this.gameObject.transform.root.CompareTag("Boss")) {
                this.gameObject.SetActive(false);

                //クリア音を鳴らす
                AudioSource.PlayClipAtPoint(clearSound, Camera.main.transform.position, 0.4f);

                //1秒後にシーン遷移のメソッドを実行
                GameObject.FindGameObjectWithTag("ScoreManager").
                    GetComponent<ScoreManager>().SaveHighScore();
                ClearMessage();
                Debug.Log("通過");
                Invoke("GoNextStage", 5);
            }
        }
    }

    /// <summary>
    /// ゲームクリアによるシーン遷移処理
    /// </summary>
    private void GoNextStage(){
        Debug.Log("通過");
        SceneManager.LoadScene("Title");
    }

    private void ClearMessage(){
        stageNumberText = GameObject.FindGameObjectWithTag("StageNumber").
            GetComponent<Text>();
        stageNumberText.color = new Color(1, 1, 1, 1);
        stageNumberText.text = "GAME CLEAR!!";
    }
}
