using UnityEngine;
using UnityEngine.UI;

public class MoleculeText : MonoBehaviour {

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
        gameObject.GetComponent<Text>().text = GameManager.chosenMolecule.Name;
    }
    
}
