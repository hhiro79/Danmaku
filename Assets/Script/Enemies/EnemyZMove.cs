using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZMove : MonoBehaviour
{
    [Range(0, 50)]
    public float moveDistance;
    [Range(0, 2)]
    public float turnSpeed;
    public bool isTurn = false;
    private int num;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchNum());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDistance * 
            Time.deltaTime * num, 0, -moveDistance * Time.deltaTime,
            Space.World);
    }

    IEnumerator SwitchNum(){
        while (this.gameObject != null) {
            if(isTurn == false) {
                num = 1;
                yield return new WaitForSeconds(turnSpeed);
                isTurn = true;
            } else{
                num = -1;
                yield return new WaitForSeconds(turnSpeed);
                isTurn = false;
            }
        }
    }
}
