using UnityEngine;

public class CarriedObject : MonoBehaviour
{

    [SerializeField] float distance;// the distance in which the object will be carried from the cam.
    [SerializeField] Canvas interactionCanvas;
    [SerializeField] GameGuide guide;

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
        if (GameManager.instance.currentStage == GameManager.Stage.stage4)
        {
            CarryGameObject();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        interactionCanvas.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Alchohol")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (GameManager.instance.chosenMolecule.GetHomogenousTeam() == Molecule.HomogenousTeam.alchohol)
                {
                    GameManager.instance.ChangeToStage(5);
                    InitialTrasform();
                }
                else
                {
                    guide.DisplayWrongMsg("That's not the correct, try again.");
                }
            }
        }
        else if (other.tag == "Ketone")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (GameManager.instance.chosenMolecule.GetHomogenousTeam() == Molecule.HomogenousTeam.ketone)
                {
                    GameManager.instance.ChangeToStage(5);
                    InitialTrasform();
                }
                else
                {
                    guide.DisplayWrongMsg("That's not the correct, try again.");
                }
            }
        }
        else if (other.tag == "Various")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (GameManager.instance.chosenMolecule.GetHomogenousTeam() == Molecule.HomogenousTeam.various)
                {
                    GameManager.instance.ChangeToStage(5);
                    InitialTrasform();
                }
                else
                {
                    guide.DisplayWrongMsg("That's not the correct, try again.");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionCanvas.enabled = false;
    }
}
