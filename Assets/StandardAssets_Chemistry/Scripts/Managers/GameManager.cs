using System;
using System.Collections.Generic;
using UnityEngine;
using goedle_sdk;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public enum Levels {moleculeNaming, moleculeConstruction,lab,menu}
    public static Levels currentLevel;

    public static Molecule chosenMolecule;

    //Molecule naming properties
    public static int namedMolecules;
    private List<string> namedMols = new List<string>();
    //molecule construction
    public static int constructedMolecules;
    private List<string> constructedMols = new List<string>();

    //PLayer Information
    public string playerName;
    public string playerClass;
    public string playerSchoolName;


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
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SlotSpawner.MolCompleted += IncrementNamedMolecules;
        EmptyParentMolecule.MolConstructed += IncrementConstructedMolecules;
    }

    private void Update()
    {
        
        if (chosenMolecule != null)
        {
          
        }
    }

    public void CreateMolecule(string name, string formula,string difficulty)
    {
        chosenMolecule = new Molecule(name, formula,difficulty);
        GoedleAnalytics.instance.track("select.molecule", name, difficulty);
    }


    #region Molecule naming

    public void IncrementNamedMolecules()
    {
        GoedleAnalytics.instance.track("complete.formula", chosenMolecule.Name);

        if (!IsMoleculeNamed(chosenMolecule.Name))
        {
            namedMolecules++;
            namedMols.Add(chosenMolecule.Name);
        }
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
        GoedleAnalytics.instance.track("complete.construction",chosenMolecule.Name);

        if (!IsMoleculeConstructed(chosenMolecule.Name))
        {
            constructedMolecules++;
            constructedMols.Add(chosenMolecule.Name);
        }
        
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


    #region Player Information

    public void SetName(string text)
    {
        instance.playerName = text.Trim();
        GoedleAnalytics.instance.trackTraits("first_name", instance.playerName);
    }
    public void SetClass(string text)
    {
        instance.playerClass = text.Trim();
        GoedleAnalytics.instance.track("group", "class", instance.playerClass);
    }
    public void SetSchoolName(string text)
    {
        instance.playerSchoolName = text.Trim();
        GoedleAnalytics.instance.track("group", "school", instance.playerSchoolName);
    }

    #endregion


    #region Exit from lab simulation

    public void ExitSimulation()
    {
        //logout user
        instance.playerName = null;
        instance.playerClass = null;
        instance.playerSchoolName = null;
        //empty molecules lists
        namedMols.Clear();
        constructedMols.Clear();
        namedMolecules = 0;
        constructedMolecules = 0;
        //make Lab scene tutorial active.
        StartCanvas.presentedToUser = false;
        currentLevel = Levels.menu;
        GoedleAnalytics.instance.track("exit.simulation");
    }

    #endregion

}
