using UnityEngine;
using UnityEngine.UI;
using goedle_sdk;

public class LogoutButton : MonoBehaviour
{

    public InputField[] inputFields;

    public void Logout()
    {
        GameManager.instance.playerName = null;
        GameManager.instance.playerClass = null;
        GameManager.instance.playerSchoolName = null;
        for (int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].text = null;
        }

        GoedleAnalytics.instance.track("logout");
        GoedleAnalytics.instance.resetUserId();
    }
}
