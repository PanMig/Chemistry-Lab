using UnityEngine;

public class Rotation3D : MonoBehaviour {

    private float rotY;
    private float rotX;
    private float rotSpeed = 200.0f;

    public bool isRotating = false;

    public void RotateWithMouse()
    {
        if (Input.GetButton("Fire2") && isRotating == false)
        {
            rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            transform.Rotate(Vector3.up * (-rotX), Space.World);
        }
    }

    public void YawRotation()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 35, Space.World);
        isRotating = true;
    }

}
