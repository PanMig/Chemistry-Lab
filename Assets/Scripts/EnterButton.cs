using UnityEngine;
using UnityEngine.UI;

public class EnterButton : CustomButton
{
    //control the value to pass to event as you need
    public string moleculeName, formula;
    public bool clicked = false;

    //sprites
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite greenSprite;
    //delegate
    public delegate void ButtonClick();
    public static event  ButtonClick ButtonClicked;


    private void Start()
    {
        clicked = false;
        gameObject.GetComponent<Image>().sprite = normalSprite;
    }

    public override void OnClick()
    {
        //create a molecule
        GameManager.instance.CreateMolecule(moleculeName, formula);
        SoundManager.instance.PlaySingle(clip);
        if (ButtonClicked != null)
        {
            ButtonClicked();
        }
        clicked = true;
        SlotSpawner.MolCompleted += ChangeSprite;
    }

    //updates also the values on GameManager because we want values to be updated only the first time the button is visited.
    public override void ChangeSprite()
    {
        if (clicked && GameManager.instance.IsMoleculeNamed(moleculeName))
        {
            gameObject.GetComponent<Image>().sprite = greenSprite;
        }
    }

}