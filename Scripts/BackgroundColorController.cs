using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorController : MonoBehaviour
{
    public Camera cam;
    public float transitionColorDuration = 1f;
    public Color winColor = Color.green;
    public Color inGameColor = Color.blue;
    public Color inGameSecondaryColor = Color.blue;
    public Color loserColor = Color.red;
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    public void changeColorInGame()
    {
        float t = Mathf.PingPong(Time.time, transitionColorDuration) / transitionColorDuration;
        cam.backgroundColor = Color.Lerp(inGameColor, inGameSecondaryColor, t);
    }

    public void chooseBackgroundColor(bool isEndGame, bool isWinner)
    {
        if (!isEndGame)
        {
            if (isWinner)
            {
                cam.backgroundColor = winColor;
            }
            else
            {
                changeColorInGame();
            }
        }
        else
        {
            if (isWinner)
            {
                cam.backgroundColor = winColor;
            }
            else
            {
                cam.backgroundColor = loserColor;
            }
        }
    }
}
