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
                GameManager.chosenMolecule = MoleculeDefinition.getActiveMolecule("naming");
                break;
            case GameManager.Levels.moleculeConstruction:
                GameManager.chosenMolecule = MoleculeDefinition.getActiveMolecule("construction");
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
                GameManager.chosenMolecule = MoleculeDefinition.nextMolecule("nameing");
                break;
            case GameManager.Levels.moleculeConstruction:
                GameManager.chosenMolecule = MoleculeDefinition.nextMolecule("construction");
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
