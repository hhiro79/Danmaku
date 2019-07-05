using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージ番号の管理をするクラス
/// </summary>
public class StageManager : MonoBehaviour
{
    //シーン遷移してもクリアされない変数
    public static int stageNum;
    [SerializeField, Header("デバッグスイッチ trueならon")]
    public bool debugSwitch;
    [SerializeField, Header("デバッグスイッチonならステージ番号上書")]
    public int debugStageNum;   

    /// <summary>
    /// Startより先に呼ばれるメソッド
    /// </summary>
    void Awake()
    {
        //現在のシーン名を取得して照合
        if(SceneManager.GetActiveScene().name == "Title") {
            PlayerHealth.currentSpecialMissile = 0;
            stageNum = 0;
        }
        if(SceneManager.GetActiveScene().name == "Stage1") {
            stageNum = 1;
        }
        //デバッグスイッチonならステージ番号を外部で指定した数字に上書
        if (debugSwitch)
        {
            stageNum = debugStageNum;
        }
    }

    /// <summary>
    /// ステージクリア　シーン遷移メソッド
    /// </summary>
    public void GoNextStage(){
        stageNum++;
        SceneManager.LoadScene(stageNum);
    }
}
