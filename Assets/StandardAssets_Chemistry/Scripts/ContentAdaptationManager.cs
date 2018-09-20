using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAdaptationManager : MonoBehaviour
{
    //public MoleculesController MoleculesCtrl;

    //delegate
    public delegate void ButtonClick();
    public static event ButtonClick NextMolecule;

    // Use this for initialization
    void Awake()
    {
        GameManager.chosenMolecule = MoleculesController.instance.GetActiveMolecule();
    }


    public void LoadNextMol(string level)
    {
        GameManager.chosenMolecule = MoleculesController.instance.NextMolecule(level);
        if (NextMolecule != null)
        {
            NextMolecule();
        }
    }
}
