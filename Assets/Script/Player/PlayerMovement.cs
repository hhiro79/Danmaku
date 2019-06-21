using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    private Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        float moveH = -CrossPlatformInputManager.GetAxis ("Horizontal") * moveSpeed;
        float moveV = -CrossPlatformInputManager.GetAxis ("Vertical") * moveSpeed;

        //float moveX = -CrossPlatformInputManager.GetAxis("Mouse X") * moveSpeed * 2.5f;
        //float moveY = -CrossPlatformInputManager.GetAxis("Mouse Y") * moveSpeed * 2.5f;

        transform.Translate (moveH, 0.0f, moveV);
        //transform.Translate (moveX, 0.0f, moveY);

        Clamp();
        Debug.Log(moveH);
        //Debug.Log(moveX);
    }


    /// <summary>
    /// プレーヤーの移動範囲を制限する
    /// </summary>
    void Clamp(){
        //プレーヤーの位置情報をposに入れる
        pos = transform.position;

        pos.x = Mathf.Clamp (pos.x, -25, 25);
        pos.z = Mathf.Clamp (pos.z, -16, 16);

        transform.position = pos;
    }
}
