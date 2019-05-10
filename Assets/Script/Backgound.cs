using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgound : MonoBehaviour
{
    private Vector3 startPosition;
    public float border;

    // Start is called before the first frame update
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

        //zの値が境界線以下になったら初期値に戻す
        if (pos.z <= border) {
            transform.position = startPosition;
        }
    }
}
