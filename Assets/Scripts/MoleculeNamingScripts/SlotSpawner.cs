using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotSpawner : MonoBehaviour
{

    [SerializeField] private int slotsNumber;
    public GameObject slotIcon;
    public GameObject parent;
    public AudioClip successClip;

    float totalWidth; //corresponding to the width of the panel that stores the slotsNumber.

    public List<Slot> slotsList = new List<Slot>();

    private void OnEnable()
    {
        //delegate usage
        EnterButton.ButtonClicked += CreateSlots;
        ExitButton.ButtonClicked += EmptySlotList;
    }

    private void OnDisable()
    {
        //delegate usage
        EnterButton.ButtonClicked -= CreateSlots;
        ExitButton.ButtonClicked -= EmptySlotList;
    }

    public delegate void MoleculeCompleted();
    public static event MoleculeCompleted MolCompleted;

    private void Update()
    {
        if (SlotsFull())
        {
            MolCompleted();
            SoundManager.instance.PlaySingle(successClip);
        }
    }


    void CreateSlots()
    {
        //delegate usage.
        ExitButton.ButtonClicked += DestroySlots;

        //calculate the number of slots from the formula string
        slotsNumber = GameManager.chosenMolecule.GetFormulaLength();
        //calculate the width of the panel that stores the slots.
        float slotWidth = slotIcon.GetComponent<RectTransform>().rect.width;
        float spacingWithAdjucent = parent.GetComponent<GridLayoutGroup>().spacing.x;
        totalWidth = slotsNumber * (slotWidth + spacingWithAdjucent);
        SetParentWidth();

        for (int i = 0; i < slotsNumber; i++)
        {
            GameObject slot = Instantiate(slotIcon, parent.transform);
            slot.GetComponent<Slot>().SlotTag = GameManager.chosenMolecule.GetFormula()[i];
        }
        // add all slots to the list ONCE.
        slotsList.AddRange(parent.GetComponentsInChildren<Slot>());

    }


    void SetParentWidth()
    {
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(totalWidth, parent.GetComponent<RectTransform>().rect.height);
    }

    public bool SlotsFull()
    {
        foreach (Slot slot in slotsList.ToArray())
        { 
            if (slot != null && slot.full)
            {
                slotsList.Remove(slot);
                if(slotsList.Count == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void DestroySlots()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
            ExitButton.ButtonClicked -= DestroySlots;
        }
    }

    public void EmptySlotList()
    {
        slotsList.Clear();
    }
}