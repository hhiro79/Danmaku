using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //private int score = 0;

    //静的変数
    //public staticをつけることで、ScoreManagerスクリプトがついている他オブジェクトと
    //scoreのデータを共有することができるようになる
    public static int score;
    public static int highScore;

    private Text scoreLabel;
    private Text highScoreLabel;
    private string HIGHSCORE = "highscore";

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Title") {
            highScore = PlayerPrefs.GetInt(HIGHSCORE, 0);
            score = 0;
        } else {
            //Textコンポーネントにアクセスして所得する
            scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
            scoreLabel.text = "Score" + score;
            highScoreLabel = GameObject.Find("HighScoreLabel").GetComponent<Text>();
            highScoreLabel.text = "HighScore" + highScore;
        }
    }
    
    /// <summary>
    /// スコアを加算するメソッド
    /// publicをつけて外部からメソッドにアクセスできるようにする
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount){

        //amountに入ってくる数値分を加算していく
        score += amount;
        if(highScore < score){
            highScore = score;
        }
        
        highScoreLabel.text = "HighScore" + highScore;
        scoreLabel.text = "Score " + score;
    }

    /// <summary>
    /// スコアデータをリセットする
    /// ハイスコアを保存
    /// ゲームオーバー時に呼ばれる
    /// </summary>
    public void ScoreReset(){
        score = 0;
        PlayerPrefs.SetInt(HIGHSCORE, highScore);
        PlayerPrefs.Save();
    }

    public void SaveHighScore(){
        PlayerPrefs.SetInt(HIGHSCORE, highScore);
        PlayerPrefs.Save();
    }
}
