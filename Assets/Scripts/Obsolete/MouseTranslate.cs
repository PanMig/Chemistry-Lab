using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.EventSystems;

public class MouseTranslate : MonoBehaviour
{

    private Camera cam;
    private Vector3 objPosition;
    private Vector3 objStartPosition;

    public bool mouseReleased = false;
    private bool collision = false;

    public bool Collision
    {
        get
        {
            return collision;
        }

        set
        {
            collision = value;
        }
    }

    private void Start()
    {
        objStartPosition = gameObject.transform.position;
        cam = Camera.main.GetComponent<Camera>();
    }

    void OnMouseDrag()
    {
        mouseReleased = false;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,1);
        objPosition = cam.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    private void OnMouseUp()
    {
        mouseReleased = true;
        if (collision == false)
        {
            print("RELEASED");
            //transform.position = objStartPosition;
        }
    }

}
