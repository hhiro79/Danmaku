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

    [SerializeField]
    public Text clearNormaTxt;
    [SerializeField]
    public Text currentNormaTxt;
    [SerializeField]
    StageManager stageManager;

    private Text snt;   //StageNumberText
    public int bonusValue_1;
    public int bonusValue_2;
    public GameObject enemyBoss;

    // Start is called before the first frame update
    void Start()
    {
        InitNorma();
    }

    // Update is called once per frame
    void Update()
    {
        if(StageManager.stageNum == 1 || StageManager.stageNum == 4){
            currentTime -= Time.deltaTime;
            currentNormaTxt.text = "あと" + currentTime.ToString("f0") + "秒";
            if(currentTime <= 0 && StageManager.stageNum == 2){
                stageManager.GoNextStage();
            }
            if(currentTime <= 0 && StageManager.stageNum == 1){
                clearNormaTxt.text = "ボスを倒せ！";
                currentNormaTxt.enabled = false;
                enemyBoss.SetActive(true);
            }
        }
        
    }

    private void InitNorma(){
        Debug.Log(StageManager.stageNum);
        switch(StageManager.stageNum){
            case 2:
                clearNormaTxt.text = "敵を" + enemyNorma + "体倒せ！";
                currentNormaTxt.text = "あと" + (enemyNorma - destroyEnemy) +
                    "体";
                break;
            case 1:
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
        if(destroyEnemy <= enemyNorma){
            destroyEnemy++;            

            if(StageManager.stageNum == 2 || StageManager.stageNum == 3){
                currentNormaTxt.text = "あと" +
                    (enemyNorma - destroyEnemy) + "体";

                if(destroyEnemy >= enemyNorma){
                    snt = GameObject.FindGameObjectWithTag("StageNumber").
                        GetComponent<Text>();
                    snt.color = new Color(1, 1, 1, 1);
                    snt.text = "STAGE CLEAR!!";
                    GameObject.FindGameObjectWithTag("Player").
                        GetComponent<PlayerHealth>().isMuteki = true;
                    //ここでリザルト表示　
                    //クリアボーナス確認　checkbonus メソッド
                    //クリア時のプレイヤーの処理をする
                    CheckBonus cb = GetComponent<CheckBonus>();
                    int bonusLank = cb.CheckClearBonus();
                    if(bonusLank == 1){
                        snt.text += "\nSPEED CLEAR BONUS 1\n" + bonusValue_1;
                        GameObject.FindGameObjectWithTag("ScoreManager").
                            GetComponent<ScoreManager>().AddScore(bonusValue_1);
                    } else if(bonusLank == 2){
                        snt.text += "\nSPEED CLEAR BONUS 2\n" + bonusValue_2;
                        GameObject.FindGameObjectWithTag("ScoreManager").
                            GetComponent<ScoreManager>().AddScore(bonusValue_2);
                    }

                    currentNormaTxt.text = "";
                    StartCoroutine(NextScene());
                }
            }
        }
    }

    private IEnumerator NextScene(){
        yield return new WaitForSeconds(5.0f);
        stageManager.GoNextStage();
    }
}
