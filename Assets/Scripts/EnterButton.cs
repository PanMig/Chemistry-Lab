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

    private void OnEnable()
    {
        SlotSpawner.MolCompleted += ChangeSprite;
    }

    private void Start()
    {
        clicked = false;
        visited = 0;
        gameObject.GetComponent<Image>().sprite = normalSprite;
    }

    public override void OnClick()
    {
        //create a molecule
        GameManager.instance.CreateMolecule(moleculeName, formula);

        if (ButtonClicked != null)
        {
            ButtonClicked();
        }
        clicked = true;
        
    }

    //updates also the values on GameManager because we want values to be updated only the first time the button is visited.
    public override void ChangeSprite()
    {
        if (clicked)
        {
            gameObject.GetComponent<Image>().sprite = greenSprite;
            visited ++;
        }
    }

}