using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeMapSizeText : MonoBehaviour
{

    private TMP_Text mapSizeText;
    private string defaultText = "Maps size :";
    private string size;
    private int mapSize = 1;

    private void Update()
    {
        switch (mapSize)
        {
            case int n when (n <= 2):
                size = "tiny";
                break;
            case int n when (n > 2 && n <= 4):
                size = "small";
                break;
            case int n when (n == 5):
                size = "default";
                break;
            case int n when (n > 5 & n < 8) :
                size = "big enough to concern !";
                break;
            case int n when (n >= 8) :
                size = "HUUUUGEEEE!!!!";
                break;
        }

        mapSizeText.text = defaultText + size;
    }
}
