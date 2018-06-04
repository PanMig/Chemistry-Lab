using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using goedle_sdk;

public class ScoreManagerDisplay : MonoBehaviour {

    public Text namedMols;
    public Text constructedMols;
    public Sprite[] badges;
    public string[] logTexts;
    public Text sumMolsNaming;
    public Text sumMolsConstruction;
    public Image badge;
    public Text logText;
    public GameObject scoreCanvas;
    public GameObject player;

    // Use this for initialization
    void Start () {
        scoreCanvas.SetActive(false);
	}

    public void DisplayScore()
    {
        namedMols.text = GameManager.namedMolecules.ToString();
        constructedMols.text = GameManager.constructedMolecules.ToString();
		sumMolsNaming.text = GameManager.instance.totalNamedMols.ToString();
		sumMolsConstruction.text = GameManager.instance.totalConstructedMols.ToString();

        if (GameManager.namedMolecules != 0 && GameManager.constructedMolecules != 0)
        {
            GoedleAnalytics.instance.track("submit.score", "nameing", ( GameManager.instance.totalNamedMols/ GameManager.namedMolecules).ToString());
            GoedleAnalytics.instance.track("submit.score", "construction", (GameManager.instance.totalConstructedMols / GameManager.constructedMolecules).ToString());
        }
		BranchScore();
        scoreCanvas.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void BranchScore()
    {
        int totalScore = GameManager.namedMolecules + GameManager.constructedMolecules;

		int firstBorder = GameManager.instance.totalNamedMols;
        int secondBorder = ((3 * firstBorder + 3 * firstBorder - 1) / 4) + 1;
        int thirdBorder = 2 * firstBorder - 1;

        if (totalScore <= firstBorder)
        {
            logText.text = logTexts[0];
            badge.sprite = badges[0];
        }
        else if(totalScore > firstBorder && totalScore <= secondBorder )
        {
            logText.text = logTexts[1];
            badge.sprite = badges[1];
        }
        else if (totalScore > secondBorder && totalScore <= thirdBorder)
        {
            logText.text = logTexts[2];
            badge.sprite = badges[2];
        }
        else
        {
            logText.text = logTexts[3];
            badge.sprite = badges[3];
        }
    }
}
