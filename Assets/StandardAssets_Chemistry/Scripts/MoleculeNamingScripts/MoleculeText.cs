using UnityEngine;
using UnityEngine.UI;

public class MoleculeText : MonoBehaviour {

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
        if (option == 1 && GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            if (GameManager.instance.IsMoleculeNamed(GameManager.chosenMolecule.Name)) { gameObject.GetComponent<Text>().color = Color.black; }
            else { gameObject.GetComponent<Text>().color = Color.white; }
        }
        else
        {
            if (GameManager.instance.IsMoleculeConstructed(GameManager.chosenMolecule.Name)) { gameObject.GetComponent<Text>().color = Color.white; }
            else { gameObject.GetComponent<Text>().color = Color.black; }
        }
    }

    private void SetText()
    {
        //stands for the naming quiz
        if (option == 1 && GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            if (GameManager.instance.IsMoleculeNamed(GameManager.chosenMolecule.Name)) {
                gameObject.GetComponent<Text>().color = new Color(0,0,0,0.4f);
            }
            else{ gameObject.GetComponent<Text>().color = Color.white; }

            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
        }
        else if(option == 1 && GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            if (GameManager.instance.IsMoleculeConstructed(GameManager.chosenMolecule.Name)) { gameObject.GetComponent<Text>().color = new Color(1, 1, 1, 0.55f); }
            else { gameObject.GetComponent<Text>().color = Color.black; }
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
        }
        else
        {
            if (GameManager.instance.IsMoleculeConstructed(GameManager.chosenMolecule.Name)) { gameObject.GetComponent<Text>().color = new Color(1,1,1,0.55f); }
            else { gameObject.GetComponent<Text>().color = Color.black; }

            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Formula;
        }
    }
    
}
