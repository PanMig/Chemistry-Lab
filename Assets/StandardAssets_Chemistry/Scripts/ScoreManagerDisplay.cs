using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ScoreManagerDisplay : MonoBehaviour {

    public Text namedMols;
    public Text constructedMols;
    public Sprite[] badges;
    public string[] logTexts;
    public Text sumMolsNaming;
    public Text sumMolsConstruction;
    public Image badge;
    public Text logText;
    public Canvas scoreCanvas;
    public GameObject player;

    // Use this for initialization
    void Start () {
        scoreCanvas.enabled = false;
	}

    public void DisplayScore()
    {
        namedMols.text = GameManager.namedMolecules.ToString();
        constructedMols.text = GameManager.constructedMolecules.ToString();
        sumMolsNaming.text = MoleculeManager.instance.standard_strategy.Count.ToString();
        sumMolsConstruction.text = MoleculeManager.instance.standard_strategy.Count.ToString();

        BranchScore();
        scoreCanvas.enabled = true;
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void BranchScore()
    {
        int totalScore = GameManager.namedMolecules + GameManager.constructedMolecules;
        if (totalScore <= 10)
        {
            logText.text = logTexts[0];
            badge.sprite = badges[0];
        }
        else if(totalScore > 10 && totalScore <= 15 )
        {
            logText.text = logTexts[1];
            badge.sprite = badges[1];
        }
        else if (totalScore > 15 && totalScore <= 19)
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
