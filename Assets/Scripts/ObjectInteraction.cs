using UnityEngine;
using goedle_sdk;

public class ObjectInteraction : MonoBehaviour
{

    private bool inTrigger = false;
    private Renderer rend;

    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private Canvas crosshair;

    [SerializeField] private Material defaultMat; //used to store the default and the outiline materials.
    [SerializeField] private Material outlinedMat;

    private enum Interactables {naming, construction,exit}
    [SerializeField] private Interactables interactable;

    public SceneLoader sceneLoader;
    public CursorLock cursor;
    public AudioClip clip;


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
        if (interactable == Interactables.naming)
        {
            GameManager.currentLevel = GameManager.Levels.moleculeNaming;
            cursor.UnLockCursor();
            string sceneName = "MoleculeNaming";
            GoedleAnalytics.track("choose.quiz",sceneName);
            sceneLoader.LoadScene(sceneName);
        }
        else if (interactable == Interactables.construction)
        {
            GameManager.currentLevel = GameManager.Levels.moleculeConstruction;
            cursor.UnLockCursor();
            string sceneName = "MoleculeConstruction";
            sceneLoader.LoadScene("MoleculeConstruction");
            GoedleAnalytics.track("choose.quiz", sceneName);
        }
        else if (interactable == Interactables.exit)
        {
            GameManager.instance.ExitSimulation();
            cursor.UnLockCursor();
            sceneLoader.LoadScene("Menu");
        }
        SoundManager.instance.PlaySingle(clip);
    }

    #region trigger with player

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoedleAnalytics.track("play");

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
