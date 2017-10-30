using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private MouseLook mouseLock;
    private int zoomSpeed = 5;

    private void Start()
    {
        DisableCamera();
    }

    private void Update()
    {
        if (GameManager.instance.currentStage == GameManager.Stage.stage3)  SubCameraZoom();
    }

    public void EnableCamera(Transform target)
    {
        DisablePlayerComponents(true);
        gameObject.transform.position = target.position;
        gameObject.transform.rotation = target.rotation;
        gameObject.GetComponent<Camera>().enabled = true;
        mouseLock.SetCursorLock(false);
        mouseLock.UpdateCursorLock();
    }

    public void DisableCamera()
    {
        DisablePlayerComponents(false);
        gameObject.GetComponent<Camera>().enabled = false;
        mouseLock.SetCursorLock(true);
        mouseLock.UpdateCursorLock();
    }

    public void SubCameraZoom()
    {
        gameObject.GetComponent<Camera>().fieldOfView -= Input.mouseScrollDelta.y * zoomSpeed;
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.Clamp(gameObject.GetComponent<Camera>().fieldOfView, 40, 90);
    }

    public void DisablePlayerComponents(bool disabled)
    {
        if (disabled)
        {
            player.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            player.GetComponent<FirstPersonController>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
