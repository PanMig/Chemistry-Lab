using UnityEngine;
using UnityEngine.UI;

public class MoleculeTextTemplate : MonoBehaviour {

    public int option;

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
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
        }
        else if(option == 1 && GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
        }
        else // for the formula in construction scene.
        {
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Formula;
        }
    }
    
}
