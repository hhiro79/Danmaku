using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyXMove2 : MonoBehaviour
{
    [Range(0, 50)]
    public float moveDistance;
    private Vector3 pos;
    private bool isReturn = false;

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        if(pos.z < 5 && isReturn == false)        
        {

            transform.Translate(-moveDistance * Time.deltaTime,
                0, moveDistance * Time.deltaTime, Space.World);
        }
        else //pos.が1以下になったとき、進行方向が変化する
        {
            isReturn = true;

            transform.Translate(-moveDistance * Time.deltaTime,
                0, -moveDistance * Time.deltaTime, Space.World);
        }
    }
}
