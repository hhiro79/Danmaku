using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop_Go : MonoBehaviour
{
    public float startSpeed_Min = 60;
    public float startSpeed_Max= 300;
    public float nextSpeed;

    private Rigidbody rb;
    private GameObject target;

    private float timeCount = 0;
    public float stopTime = 3;  //弾生成後、何秒後に停止するか

    private float stopTimeCount = 0;
    private float nextStartTime = 2;    //弾が停止後、何秒後に動き出すか

    private bool stopKey = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * Random.Range(startSpeed_Min,
            startSpeed_Max));
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (!target)
        {
            return;
        }

        timeCount += Time.deltaTime;

        if(timeCount >= stopTime && !stopKey){
            stopTimeCount += Time.deltaTime;
            rb.velocity = Vector3.zero; //弾の速度を0に＝停止させる

            //弾の色を変える
            GetComponent<MeshRenderer>().material.color = Color.white;

            if(stopTimeCount >= nextStartTime){
                this.gameObject.transform.LookAt
                    (target.transform.position);    //プレーヤー方向へ
                rb.AddForce(transform.forward * nextSpeed);
                stopKey = true;
            }
        }
    }
}
