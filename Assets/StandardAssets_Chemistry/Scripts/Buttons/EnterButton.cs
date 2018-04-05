using UnityEngine;
using UnityEngine.UI;

public class EnterButton : CustomButton
{
    //control the value to pass to event as you need
    public string moleculeName, formula,difficulty;
    public bool clicked = false;

    //sprites
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite greySprite;
    //delegate
    public delegate void ButtonClick();
    public static event ButtonClick ButtonClicked;


    private void Start()
    {
        clicked = false;
        #region Naming
        if (GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            if (GameManager.instance.IsMoleculeNamed(moleculeName))
            {
                gameObject.GetComponent<Image>().sprite = greySprite;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = normalSprite;
            }
        }
        #endregion

        #region Construction
        if (GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            if (GameManager.instance.IsMoleculeConstructed(moleculeName))
            {
                gameObject.GetComponent<Image>().sprite = greySprite;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = normalSprite;
            }
        }
        #endregion
    }


    public override void OnClick()
    {
        //create a molecule
        GameManager.instance.CreateMolecule(moleculeName, formula,difficulty);
        SoundManager.instance.PlaySingle(clip);
        if (ButtonClicked != null)
        {
            ButtonClicked();
        }
        clicked = true;
        ExitButton.ButtonClicked += ChangeSprite;
    }

    public override void ChangeSprite()
    {
        if(GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            if (clicked && GameManager.instance.IsMoleculeNamed(moleculeName))
            {
                if (gameObject != null)
                {
                    gameObject.GetComponent<Image>().sprite = greySprite;
                }
                ExitButton.ButtonClicked -= ChangeSprite;
            }
            else if (clicked)
            { // case where the button is click and then user returns to clipboard (used for emptying the delegate).
                ExitButton.ButtonClicked -= ChangeSprite;
            }
        }
        else if (GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            if (clicked && GameManager.instance.IsMoleculeConstructed(moleculeName))
            {
                if (gameObject != null)
                {
                    gameObject.GetComponent<Image>().sprite = greySprite;
                }
                ExitButton.ButtonClicked -= ChangeSprite;
            }
            else if (clicked)
            { // case where the button is click and then user returns to clipboard (used for emptying the delegate).
                ExitButton.ButtonClicked -= ChangeSprite;
            }
        }
       
    }

}