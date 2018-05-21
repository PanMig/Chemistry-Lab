using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAdaptationManager : MonoBehaviour
{
    public MoleculesController MoleculesCtrl;

    //delegate
    public delegate void ButtonClick();
    public static event ButtonClick NextMolecule;

    // Use this for initialization
    void Awake()
    {
        GameManager.chosenMolecule = MoleculesCtrl.GetActiveMolecule();
    }


    public void LoadNextMol()
    {
        GameManager.chosenMolecule = MoleculesCtrl.NextMolecule();
        if (NextMolecule != null)
        {
            NextMolecule();
        }
    }
}
