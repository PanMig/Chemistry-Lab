using UnityEngine;
using UnityEngine.UI;

public class ExitButton : CustomButton
{
    public SlotSpawner slotSpawner;

    //delegate
    public delegate void ButtonClick();
    public static event  ButtonClick ButtonClicked;

    private void Start()
    {
        SlotSpawner.MolCompleted += ChangeColor;
    }

    public override void OnClick()
    {
        if (ButtonClicked != null)
        {
            ButtonClicked();
        }
        GameManager.chosenMolecule = null;
        gameObject.GetComponent<Image>().color = Color.red;

    }

    public override void ChangeColor()
    {
        gameObject.GetComponent<Image>().color = Color.green;
    }

}