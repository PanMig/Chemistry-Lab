using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using goedle_sdk;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    private string elementCardTag;

    public bool full;

    #region properties
    public GameObject SlotItem
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    private string slotTag;
    public string SlotTag
    {
        get
        {
            return slotTag;
        }

        set
        {
            slotTag = value;
        }
    }
    #endregion

    private void Start()
    {
        ChangeImageAlpha(1.0f);
        full = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!SlotItem)
        {

            elementCardTag = Draggable.item.GetComponent<ElementCardDisplay>().GetTag();
            if (string.Equals(elementCardTag, SlotTag))
            {
                Draggable.item.transform.SetParent(transform);
                Draggable.item.GetComponent<Draggable>().placed = true;
                ChangeImageAlpha(1.0f);
                full = true;
            }
        }
    }

    #region IPointerHandler INTERFACE IMPLEMETATION

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SlotItem) ChangeImageAlpha(0.6f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeImageAlpha(1.0f);
        if (!SlotItem) ChangeImageAlpha(1.0f);
    }

    #endregion

    public void ChangeImageAlpha(float alpha)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = alpha;
    }

}
