using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCardDisplay : MonoBehaviour {

    public ElementCard element;

    private Image icon;
    private Text text;


	// Use this for initialization
	void Start () {
        icon = gameObject.GetComponent<Image>();
        text = gameObject.GetComponentInChildren<Text>();

        icon.sprite = element.icon;
        text.text = element.tag;
	}

    public string GetTag()
    {
        return element.tag;
    }
}
