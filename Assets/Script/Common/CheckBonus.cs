using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBonus : MonoBehaviour
{
    private float timer;
    public float clearLevel_1 = 40.0f;
    public float clearLevel_2 = 20.0f;
    public int bonusLank;

    void Start(){
        timer = 0;
        bonusLank = 0;
    }

    void Update(){
        timer += Time.deltaTime;
        if(timer <= clearLevel_1){
            bonusLank = 1;
        }
        if(timer <= clearLevel_2){
            bonusLank = 2;
        }
    }

    public int CheckClearBonus(){
        return bonusLank;
    }

}
