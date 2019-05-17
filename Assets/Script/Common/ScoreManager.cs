using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //private int score = 0;

    //静的変数
    //public staticをつけることで、ScoreManagerスクリプトがついている他オブジェクトと
    //scoreのデータを共有することができるようになる
    public static int score = 0;

    private Text scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        //Textコンポーネントにアクセスして所得する
        scoreLabel = this.gameObject.GetComponent<Text>();
        scoreLabel.text = "Score" + score;
    }
    
    /// <summary>
    /// スコアを加算するメソッド
    /// publicをつけて外部からメソッドにアクセスできるようにする
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount){

        //amountに入ってくる数値分を加算していく
        score += amount;

        scoreLabel.text = "Score " + score;
    }

    /// <summary>
    /// スコアデータをリセットする
    /// </summary>
    public void ScoreReset(){
        score = 0;
    }
}
