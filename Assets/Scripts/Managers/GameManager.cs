using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public enum Levels {moleculeNaming, moleculeConstruction, labSafety}
    public static Levels currentLevel;

    public static Molecule chosenMolecule;

    //Molecule naming properties
    public static int namedMolecules;
    private List<string> namedMols = new List<string>();
    //molecule construction
    public static int constructedMolecules;
    private List<string> constructedMols = new List<string>();


    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SlotSpawner.MolCompleted += IncrementNamedMolecules;
        EmptyParentMolecule.MolConstructed += IncrementConstructedMolecules;
    }

    private void Update()
    {
        
        if (chosenMolecule != null)
        {
            //print(constructedMolecules);
        }
    }

    public void CreateMolecule(string name, string formula)
    {
        chosenMolecule = new Molecule(name, formula);
    }

    #region Molecule naming

    public void IncrementNamedMolecules()
    {
        if (!IsMoleculeNamed(chosenMolecule.Name))
        {
            namedMolecules++;
        }
        namedMols.Add(chosenMolecule.Name);
    }

    public bool IsMoleculeNamed(string name)
    {
        foreach (string moleculeName in namedMols)
        {
            if (name == moleculeName)
            {
                return true;
            }
        }
        return false;
    }

    #endregion


    #region Molecule construction

    public void IncrementConstructedMolecules()
    {
        if (!IsMoleculeConstructed(chosenMolecule.Name))
        {
            constructedMolecules++;
        }
        constructedMols.Add(chosenMolecule.Name);
    }

    public bool IsMoleculeConstructed(string name)
    {
        foreach (string moleculeName in constructedMols)
        {
            if (name == moleculeName)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}
