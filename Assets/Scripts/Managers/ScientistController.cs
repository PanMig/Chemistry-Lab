using UnityEngine;
using UnityEngine.UI;

public class ScientistController : MonoBehaviour
{

    private Animator animator;
    public Text text;

    // Use this for initialization
    void OnEnable()
    {
        if(GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            SlotSpawner.MolCompleted += SetText;
        }
        else
        {
            EmptyParentMolecule.MolConstructed += SetText;
        }
        SetText();
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = true;
    }

    public void SetText()
    {
        if (GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            if (GameManager.namedMolecules <1)
            {
                text.text = "Welcome to molecule naming mini game.\n\n" +
                "Choose from a list of availiable molecules on the left and provide their formulas.";
            }
            else if (GameManager.namedMolecules == 1)
            {
                text.text = "Good job, you have just completed your first formula naming challenge.\nKeep going!!";
            }
            else
            {
                text.text = "You are getting very good at this, I'm impressed.";
            }
            SlotSpawner.MolCompleted -= SetText;
        }
        else
        {
            if (GameManager.constructedMolecules < 1)
            {
                text.text = "Welcome to molecule construction mini game.\n\n" +
                "Choose from a list of availiable molecules on the left and construct their structure.";
            }
            else if (GameManager.constructedMolecules == 1)
            {
                text.text = "Good job, you have just constructed your first molecule.\nKeep going!!";
            }
            else
            {
                text.text = "You are getting very good at this, I'm impressed.";
            }
            EmptyParentMolecule.MolConstructed -= SetText;
        }
    }


}
