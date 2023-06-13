using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour
{
    float multiplierNumber = 1f;
    bool isEndGame = false;
    bool isWinner = false;

    public float timeToUpdateMultiplier = 1f;
    public TMPro.TextMeshProUGUI multiplierText;
    public TMPro.TextMeshProUGUI winnerMultiplierText;
    public GameObject rocketElements;
    public Canvas canvas;
    public GameObject rocketPath;
    public GameObject rocketPathSelected;
    public Button restartButton;
    public Button stopButton;
    public float balance = 1000;
    public float chanceToWin = 70f; // Um numero de 0 a 100 representando a chance de vencer
    
    public Camera cam;
    public float transitionColorDuration = 1f;
    public Color winColor = Color.green;
    public Color inGameColor = Color.blue;
    public Color inGameSecondaryColor = Color.blue;
    public Color loserColor = Color.red;
    public Color rocketPathSelectedColor = Color.blue;

    void Start()
    {
        initGame();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void Update()
    {
        chooseBackgroundColor();
        chooseButtonToShow();
    }

    IEnumerator UpdateMultiplier()
	{
        while (!isEndGame)
		{
            yield return new WaitForSeconds(timeToUpdateMultiplier);
            multiplierNumber += .1f;
            UpdateMultiplierText(multiplierNumber);
            canStopGame();
        }
	}

    public void initGame()
	{
        resetGame();
        StartCoroutine(UpdateMultiplier());
        animateRocket();
    }

    public void animateRocket()
	{
        rocketElements.transform.DOMoveY(300, 30f);
    }

    public void resetGame()
	{
        isEndGame = false;
        isWinner = false;
        multiplierNumber = 1f;
        UpdateMultiplierText(multiplierNumber);
        winnerMultiplierText.text = "";
        
        rocketPathSelected.transform.SetParent(rocketElements.transform);
        //rocketPathSelected.GetComponent<RawImage>().color = Color.white;
        rocketPath.GetComponent<RawImage>().color = Color.white;

        rocketPathSelected.transform.position = new Vector2(rocketPath.transform.position.x, rocketPath.transform.position.y);

        rocketElements.transform.DOMoveY(0, 0);
    }

    void UpdateMultiplierText(float newValue)
	{
        multiplierText.text = newValue.ToString("F1").Replace(",", ".") + "x";
	}

    void changeColorInGame()
	{
        float t = Mathf.PingPong(Time.time, transitionColorDuration) / transitionColorDuration;
        cam.backgroundColor = Color.Lerp(inGameColor, inGameSecondaryColor, t);
    }
    void chooseBackgroundColor()
	{
        if (!isEndGame)
		{
            if (isWinner)
			{
                cam.backgroundColor = winColor;
            } else
			{
                changeColorInGame();
			}
		} else
		{
            if (isWinner)
			{
                cam.backgroundColor = winColor;
			} else
			{
                cam.backgroundColor = loserColor;
			}
		}
	}

    void chooseButtonToShow()
	{
        if (isEndGame)
		{
            restartButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
            stopButton.GetComponent<Button>().interactable = true;
        }
        else
		{
            restartButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);

            if (isWinner)
			{
                stopButton.GetComponent<Button>().interactable = false;
                //stopButton.gameObject.SetActive(false);
			}
        }
	}

    public void playerStopGame()
	{
        isWinner = true;
        winnerMultiplierText.text = multiplierNumber.ToString("F1").Replace(",", ".") + "x";
        rocketPathSelected.transform.SetParent(canvas.transform);
        rocketPath.GetComponent<RawImage>().color = rocketPathSelectedColor;
        // rocketPathSelected.GetComponent<RawImage>().color = rocketPathSelectedColor;
    }
    void canStopGame()
	{
        float randomNumber = Random.Range(0, 100);
        if (randomNumber > chanceToWin)
		{
            isEndGame = true;
            rocketElements.transform.DOPause();
        }
	}
}
