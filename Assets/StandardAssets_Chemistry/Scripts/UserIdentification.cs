using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                
using System.Linq;
using goedle_sdk;
using goedle_sdk.detail;

public class UserIdentification : MonoBehaviour {

    public InputField[] inputFields;
    public GameObject textLog;
    public SceneLoader sceneLoader;

    private void Start()
    {
        textLog.SetActive(false);
    }


    public void StartSimulation(string sceneName)
    {
		string user_id_raw = GameManager.instance.playerName + GameManager.instance.playerClass + GameManager.instance.playerSchoolName;
        // Creating a hashed user id, md5 hash of a string and then using a guid
		string hashed_user_id = GoedleUtils.userHash(user_id_raw);
        goedle_sdk.GoedleAnalytics.instance.setUserId(hashed_user_id);
        sceneLoader.LoadScene(sceneName);
    }

}
