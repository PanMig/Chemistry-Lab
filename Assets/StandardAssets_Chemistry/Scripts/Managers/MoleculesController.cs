using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using goedle_sdk;


public class MoleculesController : MonoBehaviour
{

    public string[] defaulStrategy;
    /* There is no need for predifined molecules,
     * I create them based on the list of molecules given from the inspector
    Molecule _H20 = new Molecule("Water", "H2O");
    Molecule _CH4 = new Molecule("Methane", "CH4");
    Molecule _HCl = new Molecule("Hydrogen Chloride", "HCl");
    Molecule _NaCl = new Molecule("Sodium Chloride", "NaCl");
    Molecule _CH4O = new Molecule("Methanole", "CH3OH");
    Molecule _C2H6O = new Molecule("Ethanol", "C2H5OH");
    Molecule _C3H6O = new Molecule("Acetone", "C3H6O");
    Molecule _CH5N = new Molecule("Methanamine", "CH5N");
    Molecule _CH2N2 = new Molecule("Cyanamide", "CH2N2");
    Molecule _C2H2O = new Molecule("Ethenone", "C2H2O");
    Molecule _NH3 = new Molecule("Ammonia", "NH3");
    Molecule _C3H8 = new Molecule("Propane", "C3H8");
    */
    
    //all the molecules that will be available in both naming and construction and
    [SerializeField] public List<Molecule> availableMolecules = new List<Molecule>();

    //Data structures
    public List<string> strategy = null;
    public List<string> received_strategy = null;
    public int strategy_count = 0;
    public Queue<string> strategy_stack = null;

    //the present molecule, made static so not to be create every time we load the script.
    private static string lastViewed_naming = null;
    private static string lastViewed_construction = null;


    //No need to have this method, I just made one to itarate through the list
    /*public Molecule GetMolecule(string formula)
    {
        switch (formula)
        {
            case "H20": return _H20;
            case "CH4": return _CH4;
            case "HCl": return _HCl;
            case "NaCl": return _NaCl;
            case "CH3OH": return _CH4O;
            case "C2H5OH": return _C2H6O;
            case "C3H6O": return _C3H6O;
            case "CH5N": return _CH5N;
            case "CH2N2": return _CH2N2;
            case "C2H2O": return _C2H2O;
            case "NH3": return _NH3;
            case "C3H8": return _C3H8;
            // I had no better idea, now water is the default molecule
            default: return _H20;
        }
    }*/

    public Molecule GetMolecule(string formula)
    {
        foreach (Molecule molecule in availableMolecules)
        {
            if(molecule.formula == formula)
            {
                return new Molecule(molecule.name, molecule.formula);
            }
        }
        return new Molecule("Molecule does not exist either in the strategy array or available molecules list","HHH");  
    }



    public void SetStrategy(string[] new_strategy)
    {
        strategy = new List<string>(new_strategy);
    }

    // this builds the strategy que for the naming quiz
    public void BuildStrategyQueue(List<string> strategy)
    {
        strategy_stack = new Queue<string>(strategy);
    }

    // helper function to get the current active molecule for a lab
    public Molecule GetActiveMolecule()
    {
        switch (GameManager.currentLevel)
        {
            case GameManager.Levels.moleculeNaming:
                if (lastViewed_naming == null)
                {
                    return GetMolecule(strategy_stack.Peek());
                }
                else
                {
                    return GetMolecule(lastViewed_naming);
                }
            case GameManager.Levels.moleculeConstruction:
                if (lastViewed_construction == null)
                {
                    return GetMolecule(strategy_stack.Peek());
                }
                else
                {
                    return GetMolecule(lastViewed_construction);
                }
            default:
                Debug.Log("error for last_viewed molecule");
                return null;
        }
        
    }

    // helper function to get the next molecule for a lab 
    public Molecule NextMolecule()
    { 
       return DequeueMolecule(strategy_stack);
    }

    // dequeue molecule and put molecule in the queue and peek the next in queue
    public Molecule DequeueMolecule(Queue<string> strategy_stack)
    {
        string current_molecule = strategy_stack.Dequeue();
        strategy_stack.Enqueue(current_molecule);
        
        // hold the last viewed molecule, differently for the two exams.
        if(GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            lastViewed_naming = strategy_stack.Peek();
        }
        else
        {
            lastViewed_construction = strategy_stack.Peek(); 
        }

        return GetMolecule(strategy_stack.Peek());
    }

    void Awake()
    {
        strategy = new List<string>(defaulStrategy);
        BuildStrategyQueue(strategy);
        strategy_count = strategy_stack.Count;

        // request strategy from server
        GoedleAnalytics.instance.requestStrategy();
        StartCoroutine(GetStrategy());

        //save molecules to GM so to use them in the lab scene
        if(GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            GameManager.instance.totalNamedMols = strategy_count;
        }
        else if (GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            GameManager.instance.totalConstructedMols = strategy_count;
        }

    }

    public IEnumerator GetStrategy()
    {
        int c = 0;
        while (GoedleAnalytics.instance.goedle_analytics.strategy == null || c < 150)
        {
            yield return null;
            c++;
        }
        if (GoedleAnalytics.instance.gio_interface.strategy != null)
        {
            if (GoedleAnalytics.instance.gio_interface.strategy["config"] != null)
            {
                GoedleAnalytics.instance.track("received.strategy", GoedleAnalytics.instance.gio_interface.strategy["id"]);
                received_strategy = transformJSONArray(GoedleAnalytics.instance.gio_interface.strategy["config"]);
                strategy = new List<string>(received_strategy);
                BuildStrategyQueue(received_strategy);
            }
        }
        strategy_count = strategy_stack.Count;
    }

    public List<string> transformJSONArray(JSONNode jStrategy)
    {
        var sStrategy = new List<string>();
        foreach (var mol in jStrategy)
        {
            sStrategy.Add(mol.Value);
        }
        return sStrategy;
    }
}

