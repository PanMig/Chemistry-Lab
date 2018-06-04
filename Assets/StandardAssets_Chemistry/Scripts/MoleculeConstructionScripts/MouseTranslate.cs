using UnityEngine;
using System.Collections;

public class MouseTranslate : MonoBehaviour
{

    private Vector3 objPosition;
    private Vector3 objStartPosition;

    public AudioClip releaseClip;

    private void Start()
    {
        objStartPosition = gameObject.transform.position;
    }

    void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;  
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        //delete
        if (Input.GetKey(KeyCode.X))
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseUp()
    {
        transform.position = objStartPosition;
        SoundManager.instance.PlaySingle(releaseClip);
    }

}
