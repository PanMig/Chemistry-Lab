using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StartCanvas : MonoBehaviour {

    private static bool disabled = false;
    public GameObject player;

	// Use this for initialization
	void Start () {
        if (disabled == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            player.GetComponent<FirstPersonController>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) 
            || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) 
            || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.SetActive(false);
            disabled = true;
            player.GetComponent<FirstPersonController>().enabled = true;
        }
    }
}
