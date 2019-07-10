using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    private Vector3 pos;

    private float moveH;
    private float moveV;

    [SerializeField, Header("デバッグ用。onならキーボード操作可能")]
    public bool isKeyboardOn;

    void Update()
    {
        if (isKeyboardOn)
        {

            moveH = -Input.GetAxis("Horizontal") * moveSpeed * 2.5f;
            moveV = -Input.GetAxis("Vertical") * moveSpeed * 2.5f;
        }
        else
        {
            moveH = -CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
            moveV = -CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;
        }

        transform.Translate(moveH, 0.0f, moveV);

        Clamp();
        //Debug.Log(moveH);
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
