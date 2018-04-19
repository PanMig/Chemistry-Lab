using UnityEngine;

public class Rotation3D : MonoBehaviour {

    public float rotSpeed = 50.0f;
    public bool isRotating = false;

    //mouse
    private float rotX;
    private float rotY;

    //start transform.
    private Vector3 startPosition;
    private Quaternion startRotation;


    public void Start()
    {
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            YawRotation(-rotSpeed);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            YawRotation(rotSpeed);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            PitchRotation(rotSpeed);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            PitchRotation(-rotSpeed);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            ResetTransformation();
        }
        //ScroolWheel Rotation
        else if (Input.GetMouseButton(1))
        {
            rotX = Input.GetAxis("Mouse X") * (rotSpeed + 10) * Mathf.Deg2Rad;
            rotY = Input.GetAxis("Mouse Y") * (rotSpeed + 10) * Mathf.Deg2Rad;

            transform.Rotate(Vector3.up * (-rotX), Space.World);
            transform.Rotate(Vector3.right * (rotY), Space.World);
        }
        else if (Input.GetMouseButton(2))
        {
            ResetTransformation();
        }
    }

    //y axis
    public void YawRotation(float speed)
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);
        isRotating = true;
    }

    //x axis
    public void PitchRotation(float speed)
    {
        transform.Rotate(Vector3.right * Time.deltaTime * speed, Space.World);
    }

    //z axis
    public void RollRotation()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed, Space.World);
    }

    public void ResetTransformation()
    {
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
    }

}
