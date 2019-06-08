using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageNumber : MonoBehaviour
{
    [SerializeField, HeaderAttribute("ステージ名")]
    public Text stageNumberText;
    private float timer;

    void Start(){

        //現在のシーンの名前を取得してtextプロパティにセット
        stageNumberText.text = SceneManager.GetActiveScene ().name;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 3.0f){
            DestroyStageNumber();
        }
        stageNumberText.color = Color.Lerp(stageNumberText.color,
            new Color(0,0,0,0), 0.5f * Time.deltaTime);
    }

    private void DestroyStageNumber(){
        stageNumberText.text = " ";
        Destroy(this);
    }
}
