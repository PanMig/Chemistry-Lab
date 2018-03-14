using UnityEngine;
using goedle_sdk;

public class GoedleLoader : MonoBehaviour
{
    public GameObject goedleAnalytics; //GameManager prefab to instantiate.

    void Awake()
    {
        //Check if a goedleManager has already been assigned to static variable goedleManager.instance or if it's still null
        if (GoedleAnalytics.instance == null)

            //Instantiate gameManager prefab
            Instantiate(goedleAnalytics);
    }
}
