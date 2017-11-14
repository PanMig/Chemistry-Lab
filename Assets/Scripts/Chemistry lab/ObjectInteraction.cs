using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    private bool inTrigger = false;
    private Renderer rend;

    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private Canvas crosshair;

    [SerializeField] private Material defaultMat; //used to store the default and the outiline materials.
    [SerializeField] private Material outlinedMat;

    private enum Interactable { notebook, microscope }
    [SerializeField] private Interactable interactable;

    [SerializeField] private GameGuide guide;
    [SerializeField] private SoundManager soundManager;


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
        if (interactable == Interactable.notebook && GameManager.instance.currentStage == GameManager.Stage.stage0)
        {
            GameManager.instance.ChangeToStage(1);
            GameManager.LoadScene("Notebook");
        }
        else if (interactable == Interactable.microscope && GameManager.instance.currentStage == GameManager.Stage.stage2)
        {
            GameManager.instance.ChangeToStage(3);
            GameManager.LoadScene("Microscope");
        }
        else if (interactable == Interactable.microscope && GameManager.instance.currentStage == GameManager.Stage.stage0)
        {
            guide.DisplayWrongMsg("Search for the textbook first.");
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
