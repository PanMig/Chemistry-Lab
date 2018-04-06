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

        public void sendGet( string url, GoedleWebRequest gwr)
        {
            StartCoroutine(getRequest( url, gwr));
        }

        public void requestStrategy(string url, GoedleAnalytics ga, GoedleWebRequest gwr, GoedleDownloadBuffer gdb){
            StartCoroutine(getJSONResponse(url, ga, gwr, gdb));
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
        public void sendPost(string url, string authentification, GoedleWebRequest gwr, GoedleUploadHandler guh)
        {
            StartCoroutine(postJSONRequest(url, authentification, gwr, guh));
        }

        public IEnumerator getRequest(string url, GoedleWebRequest gwr)
        {
            gwr.unityWebRequest = new UnityWebRequest();

            gwr.url = url;
            gwr.method = "GET";

            using (gwr.unityWebRequest)
            {
                yield return gwr.SendWebRequest();
                if (gwr.isNetworkError || gwr.isHttpError)
                {
                    Debug.Log("{\"error\": {  \"isHttpError\": \"" + gwr.isHttpError + "\",  \"isNetworkError\": \"" + gwr.isNetworkError + "\" } }");
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
        public IEnumerator getJSONResponse(string url, GoedleAnalytics ga, GoedleWebRequest gwr, GoedleDownloadBuffer gdb)
        {
            gwr.unityWebRequest = new UnityWebRequest();
            using (gwr.unityWebRequest)
            {
                gwr.url = url;
                gwr.method = "GET";
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
                        Debug.Log("The following strategy was received: "+ gdb.text);
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
            }
        }

        public IEnumerator postJSONRequest( string url, string authentification, GoedleWebRequest gwr, GoedleUploadHandler guh)
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


