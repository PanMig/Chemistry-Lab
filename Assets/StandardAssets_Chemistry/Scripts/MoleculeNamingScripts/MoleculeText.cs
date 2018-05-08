using UnityEngine;
using UnityEngine.UI;

public class MoleculeText : MonoBehaviour {

    public int option;
    public Image line;

    private void OnEnable()
    {
        ContentAdaptationManager.NextMolecule += SetText;
    }

    private void OnDisable()
    {
        ContentAdaptationManager.NextMolecule -= SetText;
    }

    public void Start()
    {
        SetText();
    }

    private void SetText() //sets the text for the molecule name and enables the line if constructed or named.
    {
        //stands for the naming quiz
        if (option == 1 && GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            if (GameManager.instance.IsMoleculeNamed(GameManager.chosenMolecule.Name)) {
                line.enabled = true;
            }
            else{ line.enabled = false; }

            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
        }
        else if(option == 1 && GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            if (GameManager.instance.IsMoleculeConstructed(GameManager.chosenMolecule.Name)) { line.enabled = true; }
            else { line.enabled = false; }
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
        }
        else // for the formula in construction scene.
        {
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Formula;
        }
    }
    
}
