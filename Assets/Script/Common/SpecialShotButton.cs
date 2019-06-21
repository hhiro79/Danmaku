using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialShotButton : MonoBehaviour
{
    public GameObject specialButton;

    void Start()
    {
        //使用回数が残った状態でステージをクリアした場合
        //ボタンを表示する
        if (PlayerHealth.currentSpecialMissile > 0)
        {
            ActiveSpecialButton();
        }
    }

    //ボタンオブジェクトの表示・非表示を切り替えるというクラス
    public void ActiveSpecialButton()
    {
        specialButton.SetActive(true);
    }

    public void InActiveSpecialButton()
    {
        specialButton.SetActive(false);
    }
}
