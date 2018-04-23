using System.Collections;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Networking;
namespace goedle_sdk.detail
{
    public class GoedleHttpClient: MonoBehaviour 
	{
        public static GoedleHttpClient instance = null;

        public void sendGet( string url, IGoedleWebRequest gwr, bool staging)
        {
            StartCoroutine(getRequest( url, gwr, staging));
        }

        public void requestStrategy(string url, GoedleAnalytics ga, IGoedleWebRequest gwr, IGoedleDownloadBuffer gdb, bool staging){
            StartCoroutine(getJSONResponse(url, ga, gwr, gdb, staging));
        }

        /*
        public JSONNode getStrategy(IUnityWebRequests www, string url)
        {
            CoroutineWithData cd = new CoroutineWithData(this, getJSONRequest(www, url));
            yield return cd.coroutine;
            Debug.Log("result is " + cd.result);  //  'success' or 'fail'
            yield return cd.result;
            // TODO RETURN JSON from REQUEST
            //yield return StartCoroutine(getJSONRequest(www, url));
        }
        */
        public void sendPost(string url, string authentification, IGoedleWebRequest gwr, IGoedleUploadHandler guh, bool staging)
        {
            StartCoroutine(postJSONRequest(url, authentification, gwr, guh, staging));
        }

        public IEnumerator getRequest(string url, IGoedleWebRequest gwr, bool staging)
        {
            if (staging)
            {
                Debug.Log("Staging is on your get request would look like this:\n" + url);
            }
            else
            {
                gwr.unityWebRequest = new UnityWebRequest();
                using (gwr.unityWebRequest)
                {
                    gwr.method = "GET";
                    gwr.url = url;
                    yield return gwr.SendWebRequest();
                    if (gwr.isNetworkError || gwr.isHttpError)
                    {
                        Debug.Log("{\"error\": {  \"isHttpError\": \"" + gwr.isHttpError + "\",  \"isNetworkError\": \"" + gwr.isNetworkError + "\" } }");
                    }
                    yield break;
                }
            }
        }

        /*
         Returns an JSONNode object this can be accessed via:
         CoroutineWithData cd = new CoroutineWithData(this, LoadSomeStuff( ) );
         yield return cd.coroutine;
         Debug.Log("result is " + cd.result);  //  'JSONNode'
         CoroutineWithData is in GoedleUtils
         */
        public IEnumerator getJSONResponse(string url, GoedleAnalytics ga, IGoedleWebRequest gwr, IGoedleDownloadBuffer gdb, bool staging)
        {
            if (staging)
            {
                Debug.Log("Staging is on your get request would look like this:\n" + url);
            }
            else
            {
                gwr.unityWebRequest = new UnityWebRequest();
                using (gwr.unityWebRequest)
                {
                    gwr.method = "GET";
                    gwr.url = url;
                    gwr.downloadHandler = gdb.downloadHandlerBuffer;
                    yield return gwr.SendWebRequest();
                    JSONNode strategy_json = null;
                    if (gwr.isNetworkError || gwr.isHttpError)
                    {
                        Debug.Log("{\"error\": {  \"isHttpError\": \"" + gwr.isHttpError + "\",  \"isNetworkError\": \"" + gwr.isNetworkError + "\" } }");
                        strategy_json = null;
                    }
                    else
                    {
                        // Show results as text
                        try
                        {
                            Debug.Log("The following strategy was received: " + gdb.text);
                            strategy_json = JSON.Parse(gdb.text);
                        }
                        catch (Exception e)
                        {
                            Debug.Log("{\"error\": \" " + e.Message + " \"}");
                            strategy_json = null;
                        }
                        // Or retrieve results as binary data
                        //byte[] results = client.downloadHandler.data;
                    }
                    ga.strategy = strategy_json;
                    yield break;
                }
            }
        }

        public IEnumerator postJSONRequest( string url, string authentification, IGoedleWebRequest gwr, IGoedleUploadHandler guh, bool staging)
	    {
            if (staging)
            {
                Debug.Log("Staging is on you would request from this url:\n" + url + "\n The data would look like this:\n"+ guh.getDataString());
            }
            else
            {
                gwr.unityWebRequest = new UnityWebRequest();
                using (gwr.unityWebRequest)
                {
                    gwr.method = "POST";
                    gwr.url = url;
                    gwr.uploadHandler = guh.uploadHandler;
                    gwr.SetRequestHeader("Content-Type", "application/json");
                    if (!string.IsNullOrEmpty(authentification))
                        gwr.SetRequestHeader("Authorization", authentification);
                    gwr.chunkedTransfer = false;
                    yield return gwr.SendWebRequest();
                    if (gwr.isNetworkError || gwr.isHttpError)
                    {
                        Debug.Log("{\"error\": {  \"isHttpError\": \"" + gwr.isHttpError + "\",  \"isNetworkError\": \"" + gwr.isNetworkError + "\" } }");
                    }
                    yield break;
                }
            }
	    }

        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
            {
                //if not, set instance to this
                instance = this;
            }
            //If instance already exists and it's not this:
            else if (instance != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }
	}
}


