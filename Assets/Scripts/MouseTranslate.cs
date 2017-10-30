using UnityEngine;

public class MouseTranslate : MonoBehaviour
{

    private Camera cam;
    private Vector3 objPosition;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<Camera>();
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,3);
        objPosition = cam.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;

        if (Input.GetKey(KeyCode.X))
        {
            Destroy(gameObject);
        }
    }

}
