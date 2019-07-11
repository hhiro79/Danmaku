using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    private bool isPose;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPose)
            {
                isPose = true;
                Time.timeScale = 0f;
            }
            else
            {
                isPose = false;
                Time.timeScale = 1f;
            }
        }
    }
}
