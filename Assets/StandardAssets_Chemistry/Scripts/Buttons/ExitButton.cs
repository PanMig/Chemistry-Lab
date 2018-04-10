using UnityEngine;
using UnityEngine.UI;
using goedle_sdk;
public class ExitButton : CustomButton
{

    //sprites
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite greenSprite;

    //delegate
    public delegate void ButtonClick();
    public static event ButtonClick ButtonClicked;

    private Animator animator;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = normalSprite;
        if (GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            SlotSpawner.MolCompleted += ChangeSprite;
        }
        else if (GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {

            EmptyParentMolecule.MolConstructed += ChangeSprite;
        }

        //initialize the animator.
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnDisable()
    {

        SlotSpawner.MolCompleted -= ChangeSprite;
        EmptyParentMolecule.MolConstructed -= ChangeSprite;
    }

    public override void OnClick()
    {
        
        if (ButtonClicked != null)
        {
            ButtonClicked();
        }
        GameManager.chosenMolecule = null;
        SoundManager.instance.PlaySingle(clip);
        Camera.main.orthographicSize = 10;

        //set parameters to initial.
        gameObject.GetComponent<Image>().sprite = normalSprite;
        animator.enabled = false;
        GoedleAnalytics.instance.track("continue.quiz",GameManager.currentLevel.ToString());
    }

    public override void ChangeSprite()
    {
        gameObject.GetComponent<Image>().sprite = greenSprite;
        animator.enabled = true;
    }

}