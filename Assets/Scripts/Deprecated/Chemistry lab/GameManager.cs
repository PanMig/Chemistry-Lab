using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public enum Levels {moleculeNaming, moleculeConstruction, labSafety}
    public static Levels currentLevel;

    private List<string> namedMols = new List<string>(); 

    public static Molecule chosenMolecule;

    //Molecule naming properties
    public static int namedMolecules;


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
    }

    private void Update()
    {
        
        if (chosenMolecule != null)
        {
            print(namedMolecules);
        }
    }

    public void CreateMolecule(string name, string formula)
    {
        chosenMolecule = new Molecule(name, formula);
    }

    public void IncrementNamedMolecules()
    {
        if (!IsMoleculeNamed())
        {
            namedMolecules++;
        }
        namedMols.Add(chosenMolecule.Name);
    }

    public bool IsMoleculeNamed() {
        foreach (string molecule in namedMols)
        {
            if(chosenMolecule.Name == molecule)
            {
                return true;
            }
        }
        return false;
    }



    public void DestroyUnusedAtoms()
    {
        var hydrogens = GameObject.FindObjectsOfType<MouseTranslate>();

        if (hydrogens.Length != 0)
        {
            for (int i = 0; i < hydrogens.Length; i++)
            {
                    Destroy(hydrogens[i].gameObject);
            }
        }
    }


}
