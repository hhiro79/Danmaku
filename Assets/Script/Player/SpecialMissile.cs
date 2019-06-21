using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMissile : MonoBehaviour
{

    //クラスの役割
    //発射条件が揃っているか
    //発射して破壊まで
    //playerのfiremissileにアタッチ

    //回数上限（playerhealthに持たせる）
    //アイテム入手でメソッド呼び出し

    //アイテムのプレハブ
    //回数いくら増やすか（アイテムのクラスで）

    //ミサイルプレファブ
    public GameObject missilePrefab;
    public float missileSpeed;
    public AudioClip fireSound;

    //タイマー変数
    private float timer;

    //待機時間 timeCount
    public float waitCount = 5.0f;

    //撃ったフラグ
    private bool shotFlag;

    // Update is called once per frame
    void Update()
    {
        //撃ったフラグがtrueならタイマーを走らせる
        //タイマーをカウントしていって指定カウント超えたら
        //弾を撃ったフラグをfalseにする
        //タイマーカウントも0にする
        if (shotFlag)
        {
            timer += Time.deltaTime;
            if(timer >= waitCount)
            {
                shotFlag = false;
                timer = 0.0f;
            }
        }
    }

    //ボタンを押したら発射するメソッド
    public void OnSpecialShotButton()
    {
        //if文で弾を撃ってるかどうか判定し
        //撃ってたら中身の処理をしない

        //if(フラグの条件){中身}
        //弾生成する処理
        //弾を撃ったというフラグbool
        //発射したミサイルを3秒後に破壊する
        //弾を撃ったというフラグをtrueにする
        if (!shotFlag)
        {
            GameObject missile = Instantiate(missilePrefab,
                transform.position, Quaternion.identity) as GameObject;

            Rigidbody missileRb = missile.GetComponent<Rigidbody>();

            missileRb.AddForce(transform.forward * missileSpeed);

            AudioSource.PlayClipAtPoint(fireSound, transform.position);

            //SP使用回数が減算される
            PlayerHealth.currentSpecialMissile--;
            if(PlayerHealth.currentSpecialMissile == 0)
            {
                GameObject.FindGameObjectWithTag("Player").
                    GetComponent<SpecialShotButton>().InActiveSpecialButton();
            }

            shotFlag = true;

            //発射したミサイルを3秒後に破壊する
            Destroy(missile, 3.0f);
        }
    }
}
