using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgound : MonoBehaviour
{
    private Vector3 startPosition;
    public float border;
    private float timer;

    void Start()
    {
        startPosition = transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= Time.deltaTime * 5;
        transform.position = new Vector3 (pos.x, pos.y, pos.z);
        timer += Time.deltaTime;

        //zの値が境界線以下になったら初期値に戻す
        if (timer >= border) {
            transform.position = startPosition;
            timer = 0;
        }
    }
}
