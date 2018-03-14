using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserIdentification : MonoBehaviour {

    public InputField[] inputFields;
    public GameObject textLog;
    public SceneLoader sceneLoader;

    private void Start()
    {
        textLog.SetActive(false);
    }

    public bool IsInputEmpty()
    {
        foreach (InputField field in inputFields)
        {
            if (field.text.Length == 0)
            {
                return true;
            }
        }
        return false;
    }

    public void StartSimulation()
    {
        if (IsInputEmpty())
        {
            textLog.SetActive(true);
        }
        else
        {
            // if input is not empty load the lab scene.
            sceneLoader.LoadScene("Lab");
        }
    }

}
