using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public enum Stage { stage0, stage1, stage2, stage3, stage4, end }
    public Stage currentStage;

    public Molecule chosenMolecule;


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


    // Use this for initialization
    void Start() {
        InitializeValues();
    }

    void Update()
    {
        if (instance.currentStage == Stage.end)
        {
            instance.ChangeToStage(0);
        }
        if (instance.currentStage == Stage.stage0)
        {
            chosenMolecule = null;
        }
    }

    public void InitializeValues()
    {
        instance.currentStage = Stage.stage0;
    }

    public void ChangeToStage(int stageNumber) {
        switch (stageNumber)
        {
            case 0:
                instance.currentStage = Stage.stage0;
                break;
            case 1:
                instance.currentStage = Stage.stage1;
                break;
            case 2:
                instance.currentStage = Stage.stage2;
                break;
            case 3:
                instance.currentStage = Stage.stage3;
                break;
            case 4:
                instance.currentStage = Stage.stage4;
                break;
            case 5:
                instance.currentStage = Stage.end;
                break;
            default:
                break;
        }
    }

    public void SetMolecule(int moleculeNumber)
    {
        switch (moleculeNumber)
        {
            case 1:
                Molecule water = new Molecule(Molecule.Molecules.water, Color.white, "H2O", false, Molecule.HomogenousTeam.various);
                instance.chosenMolecule = water;
                break;
            case 2:
                Molecule methanole = new Molecule(Molecule.Molecules.methanole, Color.white, "CH4O", false, Molecule.HomogenousTeam.alchohol);
                instance.chosenMolecule = methanole;
                break;
            case 3:
                Molecule aithanole = new Molecule(Molecule.Molecules.ethanol, Color.white, "C2H6O", false, Molecule.HomogenousTeam.alchohol);
                instance.chosenMolecule = aithanole;
                break;
            case 4:
                Molecule propanone = new Molecule(Molecule.Molecules.propanone, Color.white, "C3H6O", false, Molecule.HomogenousTeam.ketone);
                instance.chosenMolecule = propanone;
                break;
            default:
                Debug.Log("No option was recieved");
                break;
        }
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
