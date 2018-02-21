using UnityEngine;
using UnityEngine.UI;

public class MoleculeText : MonoBehaviour {

    public int option;

    private void OnEnable()
    {
        EnterButton.ButtonClicked += SetText;
    }

    private void OnDisable()
    {
        EnterButton.ButtonClicked -= SetText;
    }

    private void SetText()
    {
        if (option == 1)
        {
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
        }
        else
        {
            gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Formula;
        }
    }
    
}
