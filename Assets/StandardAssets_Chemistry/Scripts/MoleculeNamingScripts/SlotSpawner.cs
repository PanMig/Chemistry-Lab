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

    private void OnDisable()
    {
        ContentAdaptationManager.NextMolecule -= EmptySlotList;
        ContentAdaptationManager.NextMolecule -= DestroySlots;
        ContentAdaptationManager.NextMolecule -= CreateSlots;
    }

    public delegate void MoleculeCompleted();
    public static event MoleculeCompleted MolCompleted;


    private void Start()
    {
        ContentAdaptationManager.NextMolecule += DestroySlots;
        ContentAdaptationManager.NextMolecule += EmptySlotList;
        ContentAdaptationManager.NextMolecule += CreateSlots;
        CreateSlots();
    }

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
            if (slot.full || slot == null)
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
        }
    }

    public void EmptySlotList()
    {
        slotsList.Clear();
    }
}