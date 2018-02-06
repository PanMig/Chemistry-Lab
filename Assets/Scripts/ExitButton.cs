using UnityEngine;
using UnityEngine.UI;

public class ExitButton : CustomButton
{
    public SlotSpawner slotSpawner;

    //sprites
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite greenSprite;
    
    //delegate
    public delegate void ButtonClick();
    public static event  ButtonClick ButtonClicked;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = normalSprite;
        SlotSpawner.MolCompleted += ChangeSprite;
    }

    private void OnDisable()
    {
        SlotSpawner.MolCompleted -= ChangeSprite;
    }

    public override void OnClick()
    {
        if (ButtonClicked != null)
        {
            ButtonClicked();
        }
        GameManager.chosenMolecule = null;
        gameObject.GetComponent<Image>().sprite = normalSprite;
    }

    public override void ChangeSprite()
    {
        gameObject.GetComponent<Image>().sprite = greenSprite;
    }

}