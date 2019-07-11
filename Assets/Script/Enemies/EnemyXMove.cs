using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyXMove : MonoBehaviour
{
    [Range(0, 50)]
    public float moveDistance;
    private Vector3 pos;
    private bool isReturn = false;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        pos = transform.position;

        if(pos.z > 1 && isReturn == false)        
        {
            //Translate(X, y ,z)
            //x軸がプラス →方向のベクトル
            //z軸がマイナス ↓方向のベクトル
            //この2つのベクトルを合成すると↘️方向のベクトルに
            transform.Translate(moveDistance * Time.deltaTime,
                0, -moveDistance * Time.deltaTime, Space.World);
        }
        else //pos.が1以下になったとき、進行方向が変化する
        {
            isReturn = true;

            //xがプラス →方向
            //Zがマイナス ↑方向
            //合成すると↗️方向
            transform.Translate(moveDistance * Time.deltaTime,
                0, moveDistance * Time.deltaTime, Space.World);
        }
    }
}
