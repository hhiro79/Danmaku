using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYMove : MonoBehaviour
{
    [Range (0, 50)]
    public float moveDistance;
    private Vector3 pos;
    private bool isReturn = false;

    [Range (-15, 15)]
    public float changePos = 2;
    private float random;

    void Start() {
        random = Random.Range(-15, 15);
        Debug.Log(random);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        pos = transform.position;

        if (pos.z > random && !isReturn) {
            transform.Translate(0, 0,
                -moveDistance * Time.deltaTime, Space.World);
        } else {
            isReturn = true;
            transform.Translate(moveDistance * Time.deltaTime,
                0, 0, Space.World);
        }

    }
}
