using UnityEngine;

public class Crosshair : MonoBehaviour {

    private Canvas crosshair;

	// Use this for initialization
	void Start () {
        crosshair = this.GetComponent<Canvas>(); // the script must be attached to the crosshair canvas
	}
	
	public void EnableCrosshair()
    {
        crosshair.enabled = true;
    }

    public void DisableCrosshair()
    {
        crosshair.enabled = false;
    }
}             