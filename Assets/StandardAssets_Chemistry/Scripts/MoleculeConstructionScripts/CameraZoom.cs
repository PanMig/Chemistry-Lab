using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public int zoomSpeed;
    private float m_OriginalDist;

    public void Update()
    {
        //CameraZooming();
    }

    public void CameraZooming()
    {
        gameObject.GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed;
        gameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(gameObject.GetComponent<Camera>().orthographicSize,5,20);
    }
}
