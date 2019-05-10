using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public float missileSpeed;
    public AudioClip fireSound;

    //長押し連射
    private int timeCount;

    [SerializeField, HeaderAttribute("弾の最大値")]
    public int maxPower = 100;
    private int shotPower;    
    private Slider powerSlider;

    //発射パワーの回復
    const int RecoveryTime = 10;    //パワーが回復するまでの時間
    private int counter;    //発射パワー回復までの残り時間


    /// <summary>
    /// 弾切れ発生
    /// </summary>
    void Start(){
        shotPower = maxPower;
        powerSlider = GameObject.Find ("PowerSlider").
            GetComponent<Slider>();
        powerSlider.maxValue = maxPower;
        powerSlider.value = shotPower;
    }

    // Update is called once per frame
    void Update()
    {
        //発射パワーの回復
        if(shotPower <= 0 && counter <= 0) {
            StartCoroutine (RecoverPower ());
        }

        timeCount += 1;
        
        if(Input.GetButton ("Fire1")) {

            //弾切れ発生
            if (shotPower <= 0) {
                return;
            }
            shotPower -= 1;
            powerSlider.value = shotPower;

            //数字を変えると連射の間隔を変更できる
            if(timeCount % 5 == 0){

                //プレハブからミサイルオブジェクトを作成し、
                //それをmissileという箱に入れる
                GameObject missile = Instantiate (missilePrefab,
                transform.position, Quaternion.identity)
                as GameObject;

                Rigidbody missileRb = missile.GetComponent<Rigidbody>();

                missileRb.AddForce (transform.forward * missileSpeed);

                AudioSource.PlayClipAtPoint (fireSound, transform.position);

                //発射したミサイルを2秒後に破壊する
                Destroy(missile, 2.0f);
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

            print ("全回復までの残り時間" + counter + "秒");
        }

        //発射パワーをマックスに
        shotPower = maxPower;
        powerSlider.value = shotPower;
    }

  
    /// <summary>
    /// 発射パワーのリセット
    /// プレーヤーが破壊された時、この命令ブロックが呼ばれる
    /// </summary>
    public void ResetPower(){
        shotPower = maxPower;
        powerSlider.value = shotPower;

        counter = 0;
    }
}
