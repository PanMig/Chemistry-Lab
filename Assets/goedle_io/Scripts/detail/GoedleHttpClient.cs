using System.Collections;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Networking;

namespace goedle_sdk.detail
{

    public interface IGoedleHttpClient
    {
        //JSONNode getStrategy(IUnityWebRequests www, string url);
        void sendPost(string url, string content, string authentification);
        void sendGet(string url);
        void addUnityHTTPClient(IUnityWebRequests www);
        IEnumerator getRequest(string url);
        IEnumerator getStrategy(string app_key, string api_key);
        IEnumerator postJSONRequest(string url, string content, string authentification);
    }

    public interface IUnityWebRequests
    {
        UnityWebRequest SendWebRequest();
        UnityWebRequest Post(string url, string content);
        UnityWebRequest Get(string url, string content);
        int responseCode { get; set; }
        bool isNetworkError { get; set; }
        bool isHttpError { get; set; }
        string url{ get; set; }

    }

    public class GoedleHttpClient: MonoBehaviour, IGoedleHttpClient 
	{
        IUnityWebRequests _www;

        public GoedleHttpClient(){
        }

        public void addUnityHTTPClient(IUnityWebRequests www){
            _www = www;
        }

        public void sendGet( string url)
        {
            StartCoroutine(getRequest( url));
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
        public void sendPost(string url, string content, string authentification)
        {
            StartCoroutine(postJSONRequest(url, content, authentification));
        }

        public IEnumerator getRequest(string url)
        {
            UnityWebRequest client = _www as UnityWebRequest;

            using (client = new UnityWebRequest(url, "GET"))
            {
                yield return client.SendWebRequest();
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                {
                    // Show results as text
                    //Debug.Log(client.downloadHandler.text);
                    // Or retrieve results as binary data
                    //byte[] results = client.downloadHandler.data;
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
        public IEnumerator getStrategy(string app_key, string api_key)
        {
            string url = GoedleUtils.getStrategyUrl(app_key, api_key);
            UnityWebRequest client = _www as UnityWebRequest;
            using (client = new UnityWebRequest(url, "GET"))
            {
                yield return client.SendWebRequest();
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                        yield return JSON.Parse("{\"error\": \" " + client.error + " \"}");
                }
                else
                {
                    // Show results as text
                    Debug.Log(client.downloadHandler.text);
                JSONNode strategy_json;
                    try
                    {
                        strategy_json = JSON.Parse(client.downloadHandler.text);
                    }
                    catch(Exception e){
                            strategy_json = "{\"error\": \" " + e.Message + " \"}";
 
                    }
                        yield return strategy_json;
                    // Or retrieve results as binary data
                    //byte[] results = client.downloadHandler.data;
                }
            }

        }

        public IEnumerator postJSONRequest( string url, string content ,string authentification)
	    {
            UnityWebRequest client = _www as UnityWebRequest;
            client = new UnityWebRequest(url, "POST");
            using (client)
            {
                byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(content);
                client.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                client.SetRequestHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(authentification))
                    client.SetRequestHeader("Authorization", authentification);
                client.chunkedTransfer = false;
                yield return client.SendWebRequest();
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                { 
                   //Debug.Log(content);
                }
            }
	    }

	}

}


