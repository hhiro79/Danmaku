using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Range (0,10)] //Rangeは範囲を決める属性
    public float moveXDistance;
    [Range (0,10)] //Rangeは範囲を決める属性
    public float moveZDistance;
    private float timer = 0;
    private float elapedTime;
    private float randomValue;

    void Start(){
        randomValue = Random.Range(1.5f, 3.0f);
        elapedTime = randomValue;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (moveXDistance == 0){
            MoveFront();
        }

        if(timer < elapedTime){
            MoveRight();
        } else {
            MoveLeft();
            if(timer > elapedTime * 2){
                timer = 0;
                elapedTime = Random.Range(1.5f, 3.0f);
            }
        }
    }

    void MoveFront(){
        //Space.Worldを設定すると「ワールド座標」が基準に
        //書かない場合にはオブジェクトの座標が基準になる
        //オブジェクト基準にした場合、それが回転するとまっすぐに移動しなくなる

        transform.Translate (moveXDistance, 0, -moveZDistance
          * Time.deltaTime, Space.World);
    }

    void MoveRight(){
        transform.Translate(moveXDistance * Time.deltaTime, 0,
            -moveZDistance * Time.deltaTime, Space.World);
    }

    void MoveLeft(){
        transform.Translate(-moveXDistance * Time.deltaTime, 0,
            -moveZDistance * Time.deltaTime, Space.World);
    }
}
