using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFinished : MonoBehaviour
{

    public static Text finishedText;

    private void Start()
    {
        finishedText = gameObject.GetComponent<Text>();
        finishedText.gameObject.SetActive(false);
    }

    public static void ShowFinishedText()
    {
        finishedText.gameObject.SetActive(true);
    }
    
}
