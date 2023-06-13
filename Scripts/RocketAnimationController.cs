using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketAnimationController : Singleton<RocketAnimationController>
{
    public Canvas canvas;
    public GameObject rocketElements;
    public GameObject rocketPath;
    public GameObject rocketPathSelected;
    public Color rocketPathSelectedColor = Color.blue;

    public void animateRocket()
    {
        rocketElements.transform.DOMoveY(600, 45f);
    }

    public void resetRocketAnimation()
	{
        rocketPathSelected.transform.SetParent(rocketElements.transform);
        //rocketPathSelected.GetComponent<RawImage>().color = Color.white;
        rocketPath.GetComponent<RawImage>().color = Color.white;
        rocketPathSelected.transform.position = new Vector2(rocketPath.transform.position.x, rocketPath.transform.position.y);
        rocketElements.transform.DOMoveY(0, 0);
    }

    public void playerStopGame()
	{
        rocketPathSelected.transform.SetParent(canvas.transform);
        rocketPath.GetComponent<RawImage>().color = rocketPathSelectedColor;
    }

    public void pauseAnimation()
	{
        rocketElements.transform.DOPause();
    }
}
