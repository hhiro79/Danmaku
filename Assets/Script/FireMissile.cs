using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public float missileSpeed;
    public AudioClip fireSound;

    //長押し連射
    private int timeCount;

    // Update is called once per frame
    void Update()
    {
        timeCount += 1;
        
        if(Input.GetButton ("Fire1")) {

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
}
