using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        //名前でオブジェクトを特定する
        target = GameObject.Find("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            this.gameObject.transform.LookAt (target.transform.position);        
        }
    }
}
