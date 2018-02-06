using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static GameObject item; // static so to be accesed easily.
    public bool placed = false;

    private Vector3 itemPos;

    public Transform startParent;
    public Vector3 startPosition;
    public float alpha;

    public void Start()
    {
        item = gameObject;
        startParent = item.transform.parent;
        startPosition = item.transform.position;
        //delegate usage
        ExitButton.ButtonClicked += RestorePosition;
    }

    void OnDisable()
    {
        //delegate usage
        ExitButton.ButtonClicked -= RestorePosition;
    }

    public void RestorePosition()
    {
        item = gameObject;
        item.transform.SetParent(startParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        placed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!placed) GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        item = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        /* 
         * While dragging object follows mouse so all rays hit the dragged object
         * So disabling raycast on the object enables to hit other objects while dragging. 
         */    
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        itemPos = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = itemPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        if (placed)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            item = null;
        }
        else
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            item = null;
        }
    }
}
