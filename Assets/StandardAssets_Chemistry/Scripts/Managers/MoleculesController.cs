using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using goedle_sdk;


public class MoleculesController : MonoBehaviour
{
    public static MoleculesController instance = null;
    public string[] defaulStrategy;

    //all the molecules that will be available in both naming and construction and
    [SerializeField] public List<Molecule> availableMolecules = new List<Molecule>();

    //Data structures
    public List<string> strategy_naming = null;
    public List<string> strategy_construction = null;

    public List<string> received_strategy_naming = null;
    public List<string> received_strategy_construction = null;
    public static Queue<string> strategy_stack_naming = null;
    public static Queue<string> strategy_stack_construction = null;

    public int strategy_naming_count = 0;
    public int strategy_construction_count = 0;


    //the present molecule, made static so not to be created every time we load the script.
    private static string lastViewed_naming = null;
    private static string lastViewed_construction = null;


    public void SetStrategyNaming(string[] new_strategy)
    {
        strategy_naming = new List<string>(new_strategy);
    }

    public void SetStrategyConstruction(string[] new_strategy)
    {
        strategy_construction = new List<string>(new_strategy);
    }

    // this builds the strategy que for the naming quiz
    public void BuildStrategyNamingQueue(List<string> strategy)
    {
        if (lastViewed_naming == null)
            strategy_stack_naming = new Queue<string>(strategy);
    }

    // this builds the strategy que for the construction quiz
    public void BuildStrategyConstructionQueue(List<string> strategy)
    {
        if (lastViewed_construction == null)
            strategy_stack_construction = new Queue<string>(strategy);
    }

    public Molecule GetMolecule(string formula)
    {
        foreach (Molecule molecule in availableMolecules)
        {
            if (molecule.formula == formula)
            {
                return new Molecule(molecule.name, molecule.formula);
            }
        }
        return new Molecule("Molecule does not exist either in the strategy array or available molecules list", "HHH");
    }

    // helper function to get the current active molecule for a lab
    public Molecule GetActiveMolecule()
    {
        switch (GameManager.currentLevel)
        {
            case GameManager.Levels.moleculeNaming:
                if (lastViewed_naming == null)
                {
                    lastViewed_naming = strategy_stack_naming.Peek().ToString();

                    return GetMolecule(strategy_stack_naming.Peek());
                }
                else
                {
                    return GetMolecule(lastViewed_naming);
                }
            case GameManager.Levels.moleculeConstruction:
                if (lastViewed_construction == null)
                {
                    lastViewed_construction = strategy_stack_construction.Peek().ToString();
                    return GetMolecule(strategy_stack_construction.Peek());
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
    public Molecule NextMolecule(string type_of_exam)
    {
        if (type_of_exam.Equals("naming"))
            return DequeueMolecule(strategy_stack_naming);
        else
            return DequeueMolecule(strategy_stack_construction);
    }

    // dequeue molecule and put molecule in the queue and peek the next in queue
    public Molecule DequeueMolecule(Queue<string> strategy_stack)
    {
        string current_molecule = strategy_stack.Dequeue();
        strategy_stack.Enqueue(current_molecule);

        // hold the last viewed molecule, differently for the two exams.
        if (GameManager.currentLevel == GameManager.Levels.moleculeNaming)
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

        // request strategy from server
        GoedleAnalytics.instance.requestStrategy();
        StartCoroutine(GetStrategy());

        if (strategy_naming.Count <= 0 && strategy_construction.Count <= 0)
        {
            strategy_naming = new List<string>(defaulStrategy);
            strategy_construction = new List<string>(defaulStrategy);

            BuildStrategyNamingQueue(strategy_naming);
            BuildStrategyConstructionQueue(strategy_construction);

            strategy_naming_count = strategy_stack_naming.Count;
            strategy_construction_count = strategy_stack_construction.Count;

            Debug.Log("Strategy is not received, all available molecules have been loaded");
        }

        //save molecules to GM so to use them in the lab scene
        if (GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            GameManager.instance.totalNamedMols = strategy_naming_count;
        }
        else if (GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            GameManager.instance.totalConstructedMols = strategy_construction_count;
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
                if (GoedleAnalytics.instance.gio_interface.strategy["config"].IsArray)
                {
                    if (GoedleAnalytics.instance.gio_interface.strategy["config"][0]["naming"] != null)
                    {
                        received_strategy_naming = transformJSONArray(GoedleAnalytics.instance.gio_interface.strategy["config"][0]["naming"]);
                        strategy_naming = new List<string>(received_strategy_naming);
                        BuildStrategyNamingQueue(received_strategy_naming);
                    }
                    if (GoedleAnalytics.instance.gio_interface.strategy["config"][0]["construction"] != null)
                    {
                        received_strategy_construction = transformJSONArray(GoedleAnalytics.instance.gio_interface.strategy["config"][0]["construction"]);
                        strategy_construction = new List<string>(received_strategy_construction);
                        BuildStrategyConstructionQueue(received_strategy_construction);
                    }
                }
                if (received_strategy_construction != null || received_strategy_naming != null)
                    GoedleAnalytics.instance.track("received.strategy", GoedleAnalytics.instance.gio_interface.strategy["id"]);

            }
        }
        strategy_naming_count = strategy_stack_naming.Count;
        strategy_construction_count = strategy_stack_construction.Count;

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

