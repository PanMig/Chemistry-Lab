using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(true);
	}

    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}
