using System.Collections;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

namespace goedle_sdk
{

    public interface IGoedleHttpClient
    {
        JSONNode getStrategy(string app_key, string api_key);
        void sendPost(string content, string authentification);
        IEnumerator Upload(string content, string authentification);
    }

    public class GoedleHttpClient: IGoedleHttpClient
	{
        private MonoBehaviour mono;

        public GoedleHttpClient(MonoBehaviour mono){
            this.mono = mono;
        }

        public JSONNode getStrategy(string app_key, string api_key)
        {
            mono.StartCoroutine(Download(app_key, api_key));
            return null;
        }

        public IEnumerator Download(string app_key, string api_key)
        {
            string url = detail.GoedleConstants.STRATEGY_URL_LIVE + "/" + app_key + "/" + api_key;
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log(www.downloadHandler.text);

                    // Or retrieve results as binary data
                    byte[] results = www.downloadHandler.data;
                }
            }

        }

        public void sendPost (string content, string authentification)
		{
            mono.StartCoroutine(Upload(content, authentification));
		}

        public IEnumerator Upload(string content, string authentification)
	    {
	    	string track_url = detail.GoedleConstants.TRACK_URL_LIVE;
			//byte[] bytes = Encoding.UTF8.GetBytes(pass[0]);
            using (UnityWebRequest www = new UnityWebRequest(track_url, "POST"))
            {
                byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(content);
                www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("Authorization", authentification);
                www.chunkedTransfer = false;
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {

                    Debug.Log(www.error);
                }
                else
                { 
                   Debug.Log(content);
                   //Debug.Log("Form upload complete!");
                    
                }
            }
	    }
	}
}


