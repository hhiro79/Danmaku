using System.Collections;
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

    public GameObject[] playerIcons;

    //プレーヤーが破壊された回数
    public int destroyCount = 0;

    void Start(){

        playerCurrentHP = playerHP;
        playerHPSlider = GameObject.Find ("PlayerHPSlider").
            GetComponent<Slider>();
        playerHPSlider.maxValue = playerHP;
        playerHPSlider.value = playerCurrentHP;
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag ("EnemyMissile")){

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
                this.gameObject.SetActive(false);

                //破壊された回数によって場合分け
                if(destroyCount < 3) {
                    //リトライのメソッドを1秒後に呼び出す
                    Invoke("Retry", 1.0f);
                } else {
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
        this.gameObject.SetActive(true);
        playerCurrentHP = playerHP;
        playerHPSlider.value = playerCurrentHP;
    }
}
