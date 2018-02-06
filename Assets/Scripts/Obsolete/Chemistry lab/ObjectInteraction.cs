using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    private bool inTrigger = false;
    private Renderer rend;

    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private Canvas crosshair;

    [SerializeField] private Material defaultMat; //used to store the default and the outiline materials.
    [SerializeField] private Material outlinedMat;

    private enum Interactables { notebook, microscope }
    [SerializeField] private Interactables interactable;

    [SerializeField] private SoundManager soundManager;

    public SceneLoader sceneLoader;


    // Use this for initialization
    void Start()
    {
        interactionCanvas.enabled = false;
        //apply the default material
        rend = GetComponent<Renderer>();
        rend.material = defaultMat;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger == true)
        {
            InteractWithObject();
        }
    }


    private void InteractWithObject()
    {
        if (interactable == Interactables.notebook)
        {
            sceneLoader.LoadScene("Molecule Naming Images");
        }
        else if (interactable == Interactables.microscope)
        {
            sceneLoader.LoadScene("Microscope");
        }
    }

    #region trigger with player

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material = outlinedMat;
            interactionCanvas.enabled = true;
            inTrigger = true;
            crosshair.GetComponent<Crosshair>().DisableCrosshair();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        rend.material = defaultMat;
        interactionCanvas.enabled = false;
        inTrigger = false;
        crosshair.GetComponent<Crosshair>().EnableCrosshair();
    }

    #endregion

}
