using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  クリアしたときのボーナス判定用クラス
///  全ステージクリアしたときに呼ばれます
/// </summary>
public class CheckBonus : MonoBehaviour
{
    private float timer;
    public float clearLevel_1 = 40.0f;
    public float clearLevel_2 = 20.0f;
    public int bonusLank;
    public int secretEnemyCount;    //ステージ2の固定敵の倒した数のカウント用

    void Start(){
        //初期化
        timer = 0;
        bonusLank = 0;
    }

    void Update(){
        //timer でゲームの経過時間を計測する
        timer += Time.deltaTime;
        if(timer <= clearLevel_1){
            //ボーナス設定値1の範囲内なら
            bonusLank = 1;
        }
        if(timer <= clearLevel_2){
            //ボーナス設定値2の範囲内なら
            bonusLank = 2;
        }
    }

    /// <summary>
    /// クリアボーナス判定処理
    /// ステージ1と3をクリアした時に呼ばれる
    /// </summary>
    /// <returns></returns>
    public int CheckClearBonus(){
        //updateで判定されたボーナスランクを戻す
        return bonusLank;
    }

    /// <summary>
    /// 固定敵を倒した時に加算する処理
    /// </summary>
    public void AddSecretEnemyBonus()
    {
        //加算
        secretEnemyCount++;
    }

    /// <summary>
    ///  固定敵ボーナスの判定処理ステージ2をクリアしたときに呼ばれる
    /// </summary>
    /// <param name="secretNorma"></param>
    /// <returns></returns>
    public bool CheckSecretEnemyBonus(int secretNorma)
    {
        bool normaClear = false;
        if(secretNorma <= secretEnemyCount)
        {
            normaClear = true;
        }
        return normaClear;
    }
}
