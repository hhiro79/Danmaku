using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    private Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        float moveH = -Input.GetAxis ("Horizontal") * moveSpeed;
        float moveV = -Input.GetAxis ("Vertical") * moveSpeed;
        transform.Translate (moveH, 0.0f, moveV);

        Clamp();
    }


    /// <summary>
    /// プレーヤーの移動範囲を制限する
    /// </summary>
    void Clamp(){
        //プレーヤーの位置情報をposに入れる
        pos = transform.position;

        pos.x = Mathf.Clamp (pos.x, -10, 10);
        pos.z = Mathf.Clamp (pos.z, -10, 10);

        transform.position = pos;
    }
}
