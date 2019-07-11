//ステージの番号から画面に表示するクリア条件と現在の状態の表示

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNorma : MonoBehaviour
{
    public int enemyNorma;
    private int destroyEnemy;
    public float normaTime;
    private float currentTime;
    private bool isClear;

    [SerializeField]
    public Text clearNormaTxt;
    [SerializeField]
    public Text currentNormaTxt;
    [SerializeField]
    StageManager stageManager;

    private Text stageNumberText;
    public int bonusValue_1;
    public int bonusValue_2;
    public GameObject enemyBoss;

    private bool isBonus;

    // Start is called before the first frame update
    void Start()
    {
        InitNorma();
    }

    // Update is called once per frame
    void Update()
    {
        if((!isClear && StageManager.stageNum == 2) || StageManager.stageNum == 4){
            currentTime -= Time.deltaTime;
            currentNormaTxt.text = "あと" + currentTime.ToString("f0") + "秒";

            if(currentTime <= 0 && StageManager.stageNum == 2){
                currentNormaTxt.text = " ";
                DisplayResultText();
                StartCoroutine(NextScene());
                isClear = true;
            }
            if(currentTime <= 0 && StageManager.stageNum == 4){
                clearNormaTxt.text = "ボスを倒せ！";
                currentNormaTxt.enabled = false;
                enemyBoss.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 各ステージのクリア条件を設定
    /// </summary>
    private void InitNorma(){
        Debug.Log(StageManager.stageNum);
        switch(StageManager.stageNum){
            case 1:
                clearNormaTxt.text = "敵を" + enemyNorma + "体倒せ！";
                currentNormaTxt.text = "あと" + (enemyNorma - destroyEnemy) +
                    "体";
                break;
            case 2:
                currentTime = normaTime;
                clearNormaTxt.text = normaTime + "秒間 生き残れ！";
                currentNormaTxt.text = "あと" + currentTime + "秒";
                break;
            case 3:
                clearNormaTxt.text = "敵を" + enemyNorma + "体倒せ！";
                currentNormaTxt.text = "あと" + (enemyNorma - destroyEnemy) +
                    "体";
                break;
            case 4:
                currentTime = normaTime;
                clearNormaTxt.text = normaTime + "秒間 生き残れ！";
                currentNormaTxt.text = "あと" + currentTime + "秒";
                break;
        }
    }

    public void AddDestroyCount(){
        if (!isBonus) { 
            if (destroyEnemy <= enemyNorma)
            {
                destroyEnemy++;

                if (StageManager.stageNum == 1 || StageManager.stageNum == 3)
                {
                    currentNormaTxt.text = "あと" + (enemyNorma - destroyEnemy) + "体";

                    if (destroyEnemy >= enemyNorma)
                    {
                        currentNormaTxt.text = " ";
                        DisplayResultText();
                        StartCoroutine(NextScene());
                    }
                }
            }
        }
    }

    /// <summary>
    /// ステージクリアのリザルトを表示
    /// </summary>
    private void DisplayResultText()
    {
        stageNumberText = GameObject.FindGameObjectWithTag("StageNumber").
            GetComponent<Text>();
        stageNumberText.color = new Color(1, 1, 1, 1);
        stageNumberText.text = "STAGE CLEAR!!";
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.playerState = PlayerHealth.PlayerState.MUTEKI;
        //ここでリザルト表示　
        //クリアボーナス確認　checkbonus メソッド
        //クリア時のプレイヤーの処理をする
        CheckBonus cb = GetComponent<CheckBonus>();
      
        if(StageManager.stageNum == 1 || StageManager.stageNum == 3)
        {
            int bonusLank = cb.CheckClearBonus();

            if (bonusLank == 1)
            {
                stageNumberText.text += "\nSPEED CLEAR BONUS 1\n" + bonusValue_1;
                GetComponent<ScoreManager>().AddScore(bonusValue_1);
            }
            else if (bonusLank == 2)
            {
                stageNumberText.text += "\nSPEED CLEAR BONUS 2\n" + bonusValue_2;
                GetComponent<ScoreManager>().AddScore(bonusValue_2);
            }
        }
        else if(StageManager.stageNum == 2)
        {
            bool secretBonusClear = cb.CheckSecretEnemyBonus(enemyNorma);
            if (secretBonusClear)
            {
                stageNumberText.text += "\nSECRET ENEMY BONUS\n" + bonusValue_2;
                GetComponent<ScoreManager>().AddScore(bonusValue_2);
            }
        }
        
        currentNormaTxt.text = " ";
        isBonus = true;
    }

    private IEnumerator NextScene(){
        GetComponent<ScoreManager>().SaveHighScore();
        yield return new WaitForSeconds(5.0f);
        stageManager.GoNextStage();
    }
}
