using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAdaptationManager : MonoBehaviour
{

    //delegate
    public delegate void ButtonClick();
    public static event ButtonClick NextMolecule;

    // Use this for initialization
    void Awake()
    {
        switch (GameManager.currentLevel)
        {
            case GameManager.Levels.moleculeNaming:
                GameManager.chosenMolecule = MoleculeManager.getActiveMolecule("naming");
                break;
            case GameManager.Levels.moleculeConstruction:
                GameManager.chosenMolecule = MoleculeManager.getActiveMolecule("construction");
                break;
            default:
                break;
        }
    }


    public void LoadNextMol()
    {
        switch (GameManager.currentLevel)
        {
            case GameManager.Levels.moleculeNaming:
                GameManager.chosenMolecule = MoleculeManager.nextMolecule("nameing");
                break;
            case GameManager.Levels.moleculeConstruction:
                GameManager.chosenMolecule = MoleculeManager.nextMolecule("construction");
                break;
            default:
                break;
        }
        if (NextMolecule != null)
        {
            NextMolecule();
        }
    }
}
