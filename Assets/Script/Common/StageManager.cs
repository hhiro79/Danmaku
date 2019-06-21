using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static int stageNum;


    // Start is called before the first frame update
    void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Title") {
            PlayerHealth.currentSpecialMissile = 0;
            stageNum = 0;
        }
        if(SceneManager.GetActiveScene().name == "Stage1") {
            stageNum = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ステージクリア
    //シーン遷移メソッド
    public void GoNextStage(){
        stageNum++;
        SceneManager.LoadScene(stageNum);
    }
}
