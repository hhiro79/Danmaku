using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStarMove : MonoBehaviour
{
    public float speed;
    public float plusAngle;
    public float intervalTime;
    private Rigidbody rb;
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        timeCount += Time.deltaTime;

        //もしもタイムカウントが指定時間を経過したら
        if(timeCount > intervalTime){
            //1 いったん速度を0に
            rb.velocity = Vector3.zero;

            //2 指定した角度だけ方向転換
            transform.rotation = Quaternion.Euler
                (0, transform.eulerAngles.y + plusAngle, 0);
            
            //3 方向転換が完了したら再び加速
            rb.AddForce(transform.forward * speed);

            //4 タイムカウントをリセット
            timeCount = 0;
        }
    }
}
