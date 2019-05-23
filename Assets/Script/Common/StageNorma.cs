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
    [SerializeField]
    public GameObject enemyHPSlider;

    // Start is called before the first frame update
    void Start()
    {
        InitNorma();

    }

    // Update is called once per frame
    void Update()
    {
        if(StageManager.stageNum == 2 || StageManager.stageNum == 4){
            currentTime -= Time.deltaTime;
            currentNormaTxt.text = "あと" + currentTime + "秒";
            if(currentTime <= 0 && StageManager.stageNum == 2){
                stageManager.GoNextStage();
            }
            if(currentTime <= 0 && StageManager.stageNum == 4){
                clearNormaTxt.text = "ボスを倒せ！";
                Instantiate(enemyHPSlider, enemyHPSlider.transform.position,
                    Quaternion.identity);
            }
        }
        
    }

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

    //todo enemyhelth,2から呼ぶ
    public void AddDestroyCount(){
        destroyEnemy++;
        if(destroyEnemy >= enemyNorma){
            //todo ここでリザルト表示
            //todo クリア時のプレイヤーの処理をする
            //todo PlayerHelthを取得してisMutekiをtrueにする
            currentNormaTxt.text = "";
            StartCoroutine(NextScene());
            
        }
        if(StageManager.stageNum == 1 || StageManager.stageNum == 3){
            currentNormaTxt.text = "あと" +
                (enemyNorma - destroyEnemy) + "体";
        }
    }

    private IEnumerator NextScene(){
        yield return new WaitForSeconds(5.0f);
        stageManager.GoNextStage();
    }
}
