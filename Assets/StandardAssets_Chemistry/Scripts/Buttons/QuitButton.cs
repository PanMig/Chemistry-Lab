using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
    #if UNITY_WEBGL
        this.gameObject.SetActive(false);
    #endif
    }

}
