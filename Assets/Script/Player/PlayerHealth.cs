﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip damageSound;
    public AudioClip destroySound;
    public int playerHP;
    private int playerCurrentHP;
    private Slider playerHPSlider;

    //SP弾回数上限
    //アイテム入手でメソッド呼び出し
    private int maxSpecialMissile = 5;
    public static int currentSpecialMissile;    //タイトル画面で0にする

    public GameObject[] playerIcons;

    //プレーヤーが破壊された回数
    public static int destroyCount = 0;

    private ScoreManager scoreManager;

    private FireMissile fireMissile;

    public MeshRenderer meshRenderer;

    public enum PlayerState
    {
        NORMAL,
        MUTEKI  //無敵状態
    }

    //PlayerState(enum)型の変数playerState
    public PlayerState playerState;

    void Start(){

        playerState = PlayerState.NORMAL;

        //発射パワーのリセット
        fireMissile = GameObject.Find("FireMissile").GetComponent<FireMissile>();

        //ScoreLabelオブジェクトに付いているScoreManagerスクリプトにアクセス
        scoreManager = GameObject.Find("ScoreLabel").GetComponent<ScoreManager>();

        UpdatePlayerIcons();

        playerCurrentHP = playerHP;
        playerHPSlider = GameObject.Find ("PlayerHPSlider").
            GetComponent<Slider>();
        playerHPSlider.maxValue = playerHP;
        playerHPSlider.value = playerCurrentHP;
    }

    void OnTriggerEnter(Collider col){

        //|| col.gameObject.tag == "Enemy" を追加
        if(col.gameObject.tag == "EnemyMissile"
            || col.gameObject.tag == "Enemy" 
            && playerState != PlayerState.MUTEKI){

            playerCurrentHP -= 1;
            AudioSource.PlayClipAtPoint (damageSound,
                Camera.main.transform.position, 0.2f);
            playerHPSlider.value = playerCurrentHP;
            Destroy (col.gameObject);

            if(playerCurrentHP == 0) {

                //破壊された回数を増加
                destroyCount += 1;

                UpdatePlayerIcons();

                GameObject effect = Instantiate (effectPrefab,
                    transform.position, Quaternion.identity)
                     as GameObject;
                Destroy (effect, 1.0f);
                AudioSource.PlayClipAtPoint (destroySound,
                    Camera.main.transform.position, 0.2f);

                //プレイヤーを破壊せず非アクティブ状態に
                //this.gameObject.SetActive(false);

                meshRenderer.enabled = false;

                //破壊された回数によって場合分け
                if(destroyCount < 5) {
                    //リトライのメソッドを1秒後に呼び出す
                    Invoke("Retry", 1.0f);
                } else {
                    destroyCount = 0;   //ゲームオーバーになったら残機数をリセット
                    scoreManager.ScoreReset();  //ゲームオーバーになったらスコアをリセット

                    //ゲームオーバーに遷移
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }

    /// <summary>
    /// プレーヤーの残機数を表示するメソッド
    /// </summary>
    void UpdatePlayerIcons(){

        for(int i = 0; i < playerIcons.Length; i++){
            if(destroyCount <= i){
                playerIcons[i].SetActive(true);
            } else {
                playerIcons[i].SetActive(false);
            }                
        }
    }

    /// <summary>
    /// ゲームリトライに関するメソッド
    /// </summary>
    void Retry(){

        //this.gameObject.SetActive(true);
        meshRenderer.enabled = true;
        playerCurrentHP = playerHP;
        playerHPSlider.value = playerCurrentHP;

        //無敵
        playerState = PlayerState.MUTEKI;
        Invoke("MutekiOff", 2.0f);

        //発射パワーのリセット
        fireMissile.ResetPower();
    }

    //HP回復アイテム
    public void AddHP(int amount){

        //amount分だけHP回復
        playerCurrentHP += amount;

        //最大HP超にはしない
        if(playerCurrentHP > playerHP){
            playerCurrentHP = playerHP;
        }

        //HPスライダ
        playerHPSlider.value = playerCurrentHP;
    }

    //1UPアイテム
    public void Player1Up(int amount){

        //amount分、残機を回復
        //破壊された回数destroyCountをamount分減少させる
        destroyCount -= amount;

        //最大残機数を超えないように（破壊された回数が0未満にならないように）
        if(destroyCount < 0){
            destroyCount = 0;
        }

        //残機数を表示するUI（アイコン）
        for (int i = 0; i < playerIcons.Length; i++){
            if(destroyCount <= i){
                playerIcons[i].SetActive (true);
            } else {
                playerIcons[i].SetActive(false);
            }
        }
    }

    void MutekiOff(){
        
        playerState = PlayerState.NORMAL;
    }
}
