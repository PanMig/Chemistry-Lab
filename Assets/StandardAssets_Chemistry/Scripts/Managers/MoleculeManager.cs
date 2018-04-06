using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class MoleculeManager : MonoBehaviour
{
    // Definition of molecules
    public static MoleculeManager instance = null;

    static Molecule _H20 = new Molecule("Water", "H2O");
    static Molecule _CH4 = new Molecule("Methane", "CH4");
    static Molecule _HCl = new Molecule("Hydrogen Chloride", "HCl");
    static Molecule _NaCl = new Molecule("Sodium Chloride", "NaCl");
    static Molecule _CH4O = new Molecule("Methanole", "CH4O");
    static Molecule _C2H6O = new Molecule("Ethanol", "C2H6O");
    static Molecule _C3H6O = new Molecule("Acetone", "C3H6O");
    static Molecule _CH5N = new Molecule("Isocyanic Acid", "CH5N");
    static Molecule _CH2N2 = new Molecule("Cyanamide", "CH2N2");
    static Molecule _C2H2O = new Molecule("Ethenone", "C2H2O");
    public static List<string> standard_strategy = new List<string> (new string[] { "H20", "CH4", "HCl", "NaCl", "CH4O", "C2H6O", "C3H6O", "CH5N", "CH2N2", "C2H2O" });
    public static List<string> _construction_strategy = null;
    public static List<string> _naming_strategy = null;
    public static Queue<string> _strategy_stack_default = new Queue<string>(standard_strategy);
    public static Queue<string> _strategy_stack_naming = new Queue<string>(standard_strategy);
    public static Queue<string> _strategy_stack_construction=new Queue<string>(standard_strategy);
    public static JSONNode _gio_strategy = null;


    // There are no static dictonaries in C# this is ugly but the best and efficent solution
    public static Molecule getMolecule(string forumla)
    {
        switch (forumla)
        {
            case "H20": return _H20;
            case "CH4": return _CH4;
            case "HCl": return _HCl;
            case "NaCl": return _NaCl;
            case "CH4O": return _CH4O;
            case "C2H6O": return _C2H6O;
            case "C3H6O": return _C3H6O;
            case "CH5N": return _CH5N;
            case "CH2N2": return _CH2N2;
            case "C2H2O": return _C2H2O;
                // I had no better idea, now water is the default molecule
            default: return _H20;
        }
    }
    // this builds the strategy que for the naming quiz
    public static void buildStrategyNamingQueue(List<string> strategy)
    {
        _strategy_stack_naming = new Queue<string>(strategy);
    }
    // this builds the strategy que for the construction quiz
    public static void buildStrategyConstructionQueue(List<string> strategy)
    {
        _strategy_stack_construction = new Queue<string>(strategy);
    }
    // helper function to get the current active molecule for a lab
    public static Molecule getActiveMolecule(string quiz_name)
    {
        switch (quiz_name)
        {
            case "nameing": return getMolecule(_strategy_stack_naming.Peek());
            case "construction": return getMolecule(_strategy_stack_construction.Peek());
            default: return getMolecule(_strategy_stack_default.Peek());
        }
    }
    // helper function to get the next molecule for a lab 
    public static Molecule nextMolecule(string quiz_name)
    {
        switch (quiz_name)
        {
            case "nameing": return dequeueMolecule(_strategy_stack_naming);
            case "construction": return dequeueMolecule(_strategy_stack_construction);
            default: return dequeueMolecule(_strategy_stack_default);
        }
    }
    // dequeue molecule and put molecule in the queue and peek the next in queue
    public static Molecule dequeueMolecule(Queue<string> strategy_stack){
        string current_molecule = strategy_stack.Dequeue();
        strategy_stack.Enqueue(current_molecule);
        return getMolecule(strategy_stack.Peek());
    }

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
        goedle_sdk.GoedleAnalytics.requestStrategy();
        StartCoroutine(getStrategy());
    }

    public IEnumerator getStrategy()
    {
        int c = 0;
        while (goedle_sdk.GoedleAnalytics.gio_interface.strategy == null || c < 150)
        {
            yield return null;
            c++;
        }
        if (goedle_sdk.GoedleAnalytics.gio_interface.strategy != null)
        {
            _gio_strategy = goedle_sdk.GoedleAnalytics.gio_interface.strategy;
            if (_gio_strategy["config"]["naming"] != null)
            {
                _naming_strategy = transformJSONArray(_gio_strategy["config"]["naming"]);
            }else{
                _naming_strategy = standard_strategy;

            }
            if (_gio_strategy["config"]["construction"] != null)
            {
                _construction_strategy = transformJSONArray(_gio_strategy["config"]["construction"]);
            }else{
                _construction_strategy = standard_strategy;

            }
        }
        else{
            _naming_strategy = standard_strategy;
            _construction_strategy = standard_strategy;
        }

    }

    public List<string> transformJSONArray(JSONNode jStrategy){
        var sStrategy = new List<string>();
        foreach (var mol in jStrategy.AsArray)
        {
            sStrategy.Add(mol.ToString());
        }
        return sStrategy;
    }
}

