using UnityEngine;

public class StaticObjectInteraction : MonoBehaviour {

    private Renderer rend;
    public Canvas interactionCanvas;
    [SerializeField] private Material defaultMat; //used to store the default and the outiline materials
    [SerializeField] private Material outlinedMat;
    private bool inTrigger = false;
    [SerializeField] private Transform target;
    private CameraManager cam;

    private enum Interactable {notebook,microscope}
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
        cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<CameraManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger == true)
        {
            interactionCanvas.enabled = false;
            InteractWithObject();
            inTrigger = false;
            soundManager.PlaySoundOnce(soundManager.audioClips[1]);
        }
    }


    private void InteractWithObject()
    {
        if(interactable == Interactable.notebook && GameManager.instance.currentStage == GameManager.Stage.stage0)
        {
            GameManager.instance.ChangeToStage(1);
            cam.EnableCamera(target);
        }
        else if(interactable == Interactable.microscope && GameManager.instance.currentStage == GameManager.Stage.stage2)
        {
            GameManager.instance.ChangeToStage(3);
            cam.EnableCamera(target);
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
        }
    }

    public void OnTriggerExit(Collider other)
    {
        rend.material = defaultMat;
        interactionCanvas.enabled = false;
        inTrigger = false;
    }

    #endregion
}
