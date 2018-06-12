using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour {

    public Slider experienceBar;
    public float progress;

	// Use this for initialization
	void Start () {
        CalculateExperience();
	}
	
    public void CalculateExperience()
    {
        int totalMolecules = 2 * GameManager.instance.totalNamedMols;
        int playersMolecules = GameManager.namedMolecules + GameManager.constructedMolecules;
        progress = Mathf.Clamp01 ((float)playersMolecules / (float)totalMolecules);
        GameManager.instance.playerProgress = Mathf.FloorToInt(progress*100); //store to GM in percetange.
        experienceBar.value = progress;
    }
}
