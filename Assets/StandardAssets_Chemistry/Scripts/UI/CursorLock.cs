using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CursorLock : MonoBehaviour {

    [SerializeField] private MouseLook mouseLook;

    private void Start()
    {
        LockCursor();
    }

    public void LockCursor()
    {
        mouseLook.SetCursorLock(true);
        mouseLook.UpdateCursorLock();
    }

    public void UnLockCursor()
    {
        mouseLook.SetCursorLock(false);
        mouseLook.UpdateCursorLock();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<FirstPersonController>().enabled == true)
        {
            LockCursor();
        }
    }

}
