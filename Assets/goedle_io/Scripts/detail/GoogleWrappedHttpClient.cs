using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace goedle_sdk.detail
{
    public class GoogleWrappedHttpClient
    {

        public GoogleWrappedHttpClient ()
        {

        }
         
        public void send(Dictionary<string, string> postData)
        {
            // StartCoroutine(Upload(pass));
        }
/*
        IEnumerator Upload(string [] pass)
        {
            string dataString = buildPostDataString (postData);

            using (UnityWebRequest www = UnityWebRequest.Get(dataString)
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }
    */
        /*
		public string buildPostDataString (Dictionary<string, string> postData)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append (GoedleConstants.GOOGLE_MP_TRACK_URL+"?");
			bool first = true;
			foreach(var item in postData)
			{
				if (first)
					first = false;
				else
					sb.Append('&');
				sb.Append(item.Key);
				sb.Append('=');
				sb.Append(WWW.EscapeURL(item.Value.ToString()));
			}
			return sb.ToString();
		}*/
    }
}