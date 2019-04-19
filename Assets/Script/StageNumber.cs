using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNumber : MonoBehaviour
{
    [SerializeField]
    private Text stageNumberText;

    // Update is called once per frame
    void Update()
    {
        stageNumberText.color = Color.Lerp(stageNumberText.color,
            new Color(0,0,0,0), 0.5f * Time.deltaTime);
    }
}
