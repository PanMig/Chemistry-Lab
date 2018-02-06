using UnityEngine;

public class CarriedObject : MonoBehaviour
{

    [SerializeField] float distance;// the distance in which the object will be carried from the cam.
    [SerializeField] Canvas interactionCanvas;

    private GameObject mainCam;
    private Rigidbody rigidbd;
    private Vector3 carriedPos;
    private Vector3 initialPos;
    private Quaternion initialRot;

    // Use this for initialization
    void Awake()
    {
        interactionCanvas.enabled = false;
        initialPos = gameObject.transform.position;
        initialRot = gameObject.transform.rotation;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        rigidbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CarryGameObject();
    }

    void CarryGameObject()
    {
        rigidbd.isKinematic = true;
        carriedPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
        gameObject.transform.position = carriedPos + mainCam.transform.forward * distance;
    }

    void InitialTrasform()
    {
        gameObject.transform.position = initialPos;
        gameObject.transform.rotation = initialRot;

    }



    private void OnTriggerExit(Collider other)
    {
        interactionCanvas.enabled = false;
    }
}
