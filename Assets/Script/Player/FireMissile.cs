using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class FireMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public float missileSpeed;
    public AudioClip fireSound;

    //長押し連射
    private int timeCount;

    [SerializeField, HeaderAttribute("弾の最大値")]
    public int maxPower = 100;
    public int shotPower;    
    private Slider powerSlider;

    //発射パワーの回復
    const int RecoveryTime = 5;    //パワーが回復するまでの時間
    private int counter;    //発射パワー回復までの残り時間

    //発射パワーの表示
    private Slider waitTimeSlider;

    //メインの発射口かどうか(trueならメイン)
    public bool mainPod;

    //サブポッドの許可フラグ
    public bool subPod;

    /// <summary>
    /// 弾切れ発生
    /// </summary>
    void Start(){

        if (mainPod)
        {
            shotPower = maxPower;
            powerSlider = GameObject.Find("PowerSlider").
                GetComponent<Slider>();
            powerSlider.maxValue = maxPower;
            powerSlider.value = shotPower;

            //発射パワーの表示
            waitTimeSlider = GameObject.Find("WaitTimeSlider")
                .GetComponent<Slider>();
            waitTimeSlider.maxValue = RecoveryTime;
            waitTimeSlider.value = RecoveryTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //発射パワーがMAXかどうか
        //MAXならチャージできる　ゲージの色を変える　SE
        //タイマー変数を用意し、長押しの秒数を計る
        //ボタンを押した時、離した時、長押ししてる時の3状態
        //押した瞬間は通常弾

        //発射パワーの回復
        if (mainPod) {
            if (shotPower <= 0 && counter <= 0)
            {
                StartCoroutine(RecoverPower());
            }
        }
        
        timeCount += 1;

        if (CrossPlatformInputManager.GetButton("Fire1") || Input.GetButton("Fire1")) { 
            
            //弾切れ発生
            if (shotPower <= 0) {
                return;
            }

            //メインポッドか、サブポッドならばフラグが立っていれば
            if (mainPod || !mainPod && subPod)
            {
                if (mainPod) { 
                    shotPower -= 1;
                    powerSlider.value = shotPower;
                }

                //数字を変えると連射の間隔を変更できる
                if (timeCount % 5 == 0)
                {

                    //プレハブからミサイルオブジェクトを作成し、
                    //それをmissileという箱に入れる
                    GameObject missile = Instantiate(missilePrefab,
                    transform.position, Quaternion.identity);
                    //as GameObject;

                    Rigidbody missileRb = missile.GetComponent<Rigidbody>();

                    missileRb.AddForce(transform.forward * missileSpeed);

                    AudioSource.PlayClipAtPoint(fireSound, transform.position);

                    //発射したミサイルを2秒後に破壊する
                    Destroy(missile, 2.0f);
                }
            }
        }
    }

    //発射パワーの回復
    IEnumerator RecoverPower(){

        //パワー回復までに必要な時間をセット
        counter = RecoveryTime;

        //1秒ずつカウントを進める
        while (counter > 0) {
            yield return new WaitForSeconds (1.0f);

            counter -= 1;

            //発射パワーの表示
            waitTimeSlider.value = counter;

            print ("全回復までの残り時間" + counter + "秒");
        }

        //発射パワーをマックスに
        shotPower = maxPower;
        powerSlider.value = shotPower;

        //発射パワーの表示
        //待機時間が終了したら待機ゲージを最初の状態に戻す
        waitTimeSlider.value = RecoveryTime;
    }

  
    /// <summary>
    /// 発射パワーのリセット
    /// プレーヤーが破壊された時、この命令ブロックが呼ばれる
    /// </summary>
    public void ResetPower(){
        shotPower = maxPower;
        powerSlider.value = shotPower;

        counter = 0;

        //発射パワーの表示
        //プレイヤーが破壊されたら待機ゲージを最初の状態に戻す
        waitTimeSlider.value = RecoveryTime;
    }
}
