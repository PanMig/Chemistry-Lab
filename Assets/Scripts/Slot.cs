using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    private string elementCardTag;
    private Sprite startSprite;
    public Sprite hightlightedSprite;
    public Sprite filledSprite;

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
        startSprite = gameObject.GetComponent<Image>().sprite;
        full = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!SlotItem)
        {
            elementCardTag = Draggable.item.GetComponent<ElementCardDisplay>().GetTag();
            if (string.Equals(elementCardTag, SlotTag))
            {
                ChangeImageSprite(filledSprite);
                Draggable.item.transform.SetParent(transform);
                Draggable.item.GetComponent<Draggable>().placed = true;
                full = true;
            }
        }
    }

    #region IPointerHandler INTERFACE IMPLEMETATION

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SlotItem) ChangeImageSprite(hightlightedSprite);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!SlotItem) ChangeImageSprite(startSprite);
    }

    #endregion

    public void ChangeImageSprite(Sprite sprite)
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }


}
