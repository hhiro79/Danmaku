using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8VMove : MonoBehaviour
{
    private float angle = 0;

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * 2;

        transform.position = new Vector3(
            //X
            Mathf.Sin(angle * 2) * 2,
            //Y
            transform.position.y,
            //Z
            Mathf.Sin(angle) * 12
        );
    }
}
