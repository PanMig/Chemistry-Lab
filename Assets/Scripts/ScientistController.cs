using UnityEngine;
using UnityEngine.UI;

public class ScientistController : MonoBehaviour
{

    private Animator animator;
    public Text text;

    // Use this for initialization
    void OnEnable()
    {
        SlotSpawner.MolCompleted += SetText;
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = true;
    }

    public void SetText()
    {
        if (GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            if (GameManager.namedMolecules <= 1)
            {
                text.text = "Good job, you have just completed your first formula naming challenge.\nKeep going!!";
            }
            else
            {
                text.text = "You are getting very good at this, I'm impressed.";
            }
        }
    }
}
