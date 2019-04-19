using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public float missileSpeed;
    public AudioClip fireSound;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown ("Fire1")) {

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
