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

    public Button restartButton;
    public Button stopButton;
    public float balance = 1000;
    public float chanceToWin = 70f; // Um numero de 0 a 100 representando a chance de vencer

    public BackgroundColorController backgroundColorController;

    void Start()
    {
        initGame();
    }

    void Update()
    {
        chooseButtonToShow();
        backgroundColorController.chooseBackgroundColor(isEndGame, isWinner);
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
        RocketAnimationController.instance.animateRocket();
    }

    public void resetGame()
	{
        isEndGame = false;
        isWinner = false;
        multiplierNumber = 1f;
        UpdateMultiplierText(multiplierNumber);
        winnerMultiplierText.text = "";
        RocketAnimationController.instance.resetRocketAnimation();
    }

    void UpdateMultiplierText(float newValue)
	{
        multiplierText.text = newValue.ToString("F1").Replace(",", ".") + "x";
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
        RocketAnimationController.instance.playerStopGame();
    }
    void canStopGame()
	{
        float randomNumber = Random.Range(0f, 100f);
        if (randomNumber > chanceToWin)
		{
            isEndGame = true;
            RocketAnimationController.instance.pauseAnimation();
        }
	}
}
