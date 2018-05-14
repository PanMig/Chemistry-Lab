using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Threading;
namespace goedle_sdk.detail
{
    public class GoedleAnalytics
    {
        private string _api_key = null;
        private string _app_key = null;
        private string _user_id = null;
        private string _anonymous_id = null;
        private string _app_version = null;
        private string _ga_tracking_id = null;
        private string _app_name = null;
        private int _cd1;
        private int _cd2;
        private string _cd_event = null;
        private GoedleHttpClient _gio_http_client;
        public GoedleUtils _goedleUtils = new GoedleUtils();
        public JSONNode strategy = null;
        IGoedleWebRequest _gwr = null;
        public bool _staging = false;
        public bool _adaptation_only = false;
        //private string locale = null;

        public GoedleAnalytics(
                string api_key, 
                string app_key, 
                string user_id, 
                string app_version, 
                string ga_tracking_id, 
                string app_name, 
                int cd1, 
                int cd2, 
                string cd_event, 
                GoedleHttpClient gio_http_client, 
                IGoedleWebRequest gwr, 
                IGoedleUploadHandler guh, 
                bool staging,
                bool adaptation_only)//, string locale)
        {
            _api_key = api_key;
            _app_key = app_key;
            _user_id = user_id;
            _app_version = app_version;
            _ga_tracking_id = ga_tracking_id;
            _app_name = app_name;
            _cd1 = cd1;
            _cd2 = cd2;
            _cd_event = cd_event;
            _gio_http_client = gio_http_client;
            _gwr = gwr;
            _staging = staging;
            _adaptation_only = adaptation_only;
            //this.locale = GoedleLanguageMapping.GetLanguageCode (locale);
            track_launch(guh);
        }

        public void reset_user_id(string user_id)
        {
            _user_id = user_id;
        }

        public void set_user_id(string user_id, IGoedleUploadHandler goedleUploadHandler)
        {
            _anonymous_id = _user_id;
            _user_id = user_id;
            track(GoedleConstants.IDENTIFY, null, null, false, null, null, goedleUploadHandler);
        }

        // TODO Blocking Time default  
        public void requestStrategy(IGoedleDownloadBuffer goedleDownloadBuffer)
        {
            GoedleUtils gu = new GoedleUtils();
            string url = gu.getStrategyUrl(_app_key);
            _gio_http_client.requestStrategy(url, this, _gwr, goedleDownloadBuffer, _staging);
        }

        public void track_launch(IGoedleUploadHandler goedleUploadHandler)
        {
            track(GoedleConstants.EVENT_NAME_INIT, null, null, true, null, null, goedleUploadHandler);
        }

        public void track(string event_name, string event_id, string event_value, bool launch, string trait_key, string trait_value, IGoedleUploadHandler goedleUploadHandler )
        {
            if (!_adaptation_only)
            {

                bool ga_active = !String.IsNullOrEmpty(_ga_tracking_id);
                string authentication = null;
                string content = null;
                int ts = getTimeStamp();
                // -1 because c# returns -1 for UTC +1 , * 1000 from Seconds to Milliseconds
                int timezone = (int)(((DateTime.UtcNow - DateTime.Now).TotalSeconds) * -1 * 1000);
                GoedleAtom rt = new GoedleAtom(_app_key, _user_id, ts, event_name, event_id, event_value, timezone, _app_version, _anonymous_id, trait_key, trait_value, ga_active);
                if (rt == null)
                {
                    Console.Write("Data Object is None, there must be an error in the SDK!");
                    return;
                }
                else
                {
                    content = rt.getGoedleAtomDictionary().ToString();
                    authentication = _goedleUtils.encodeToUrlParameter(content, _api_key);
                }
                string url = GoedleConstants.TRACK_URL;
				Console.WriteLine(event_name);
				Console.WriteLine(event_id);
                goedleUploadHandler.add(content);

                _gio_http_client.sendPost(url, authentication, _gwr, goedleUploadHandler, _staging);
                // Sending tp Google Analytics for now we only support the Event tracking
                string type = "event";
                if (ga_active)
                    trackGoogleAnalytics(event_name, event_id, event_value, type);
            }
        }

        private string buildGAUrlDataString(Dictionary<string, string> postData)
        {
            StringBuilder sb = new StringBuilder();
            string url = GoedleConstants.GOOGLE_MP_TRACK;

            sb.Append(url + "?");
            bool first = true;
            foreach (var item in postData)
            {
                if (first)
                    first = false;
                else
                    sb.Append('&');
                sb.Append(item.Key);
                sb.Append('=');
                sb.Append(UnityWebRequest.EscapeURL(item.Value));
            }
            return sb.ToString();
        }

        public void trackGoogleAnalytics(string event_name, string event_id, string event_value, string type)
        {
            if (string.IsNullOrEmpty(event_name)) throw new ArgumentNullException();
            // the request body we want to send
            var postData = new Dictionary<string, string>
                           {
                               {"v", GoedleConstants.GOOGLE_MP_VERSION.ToString()},
                               {"av", _app_version},
                               {"an", _app_name},
                               {"tid", _ga_tracking_id},
                               {"cid", _user_id},
                               {"t", type},
                               {"ec", getSceneName()},
                               {"ea", event_name},
								//{"ul", this.locale},
                           };


            // This is the Event label in Google Analytics

            if (!String.IsNullOrEmpty(event_id))
            {
                postData.Add("el", event_id);
            }
            if (_goedleUtils.IsFloatOrInt(event_value))
            {
                postData.Add("ev", event_value);
            }

            if (!String.IsNullOrEmpty(_anonymous_id))
            {
                postData.Add("uid", _user_id);
                // For mapping after identify
                // Otherwise we will lost the old client id
                postData.Remove("cid");
                postData.Add("cid",_anonymous_id);
            }
            if (_cd_event == "group" && event_name == "group" && _cd1 != 0 && _cd2 != 0 && _cd1 != _cd2)
            {
                postData.Remove("el");
                postData.Remove("ev");
                postData.Add(String.Concat("cd", _cd1), event_id);
                postData.Add(String.Concat("cd", _cd2), event_value);
            }
            _gio_http_client.sendGet(buildGAUrlDataString(postData), _gwr, _staging);
        }


        public void track(string event_name, IGoedleUploadHandler goedleUploadHandler)
        {
            track(event_name, null, null, false, null, null, goedleUploadHandler);
        }


        public void track(string event_name, string event_id, IGoedleUploadHandler goedleUploadHandler)
        {
            track(event_name, event_id, null, false, null, null, goedleUploadHandler);
        }

        public void track(string event_name, int event_id_i, IGoedleUploadHandler goedleUploadHandler)
        {
            string event_id = event_id_i.ToString();

            track(event_name, event_id, null, false, null, null, goedleUploadHandler);
        }

        public void track(string event_name, string event_id, string event_value, IGoedleUploadHandler goedleUploadHandler)
        {
            track(event_name, event_id, event_value, false, null, null, goedleUploadHandler);

        }

        public void trackGroup(string group_type, string group_member, IGoedleUploadHandler goedleUploadHandler)
        {
            track("group", group_type, group_member, false, null, null, goedleUploadHandler);
        }

        public void trackTraits(string trait_key, string trait_value, IGoedleUploadHandler goedleUploadHandler)
        {
            track(GoedleConstants.IDENTIFY, null, null, false, trait_key, trait_value, goedleUploadHandler);
        }

		public int getTimeStamp ()
		{
			return (Int32)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
		}

		public string getSceneName()
		{
			Scene scene = SceneManager.GetActiveScene();
            if (string.IsNullOrEmpty(scene.name))
            {
                return "NoScence";
            }
            else
            {
                return scene.name;
            }
		}

		private enum HitType
        {
            // ReSharper disable InconsistentNaming
            @event,
            @pageview,
            // ReSharper restore InconsistentNaming
        }



	}
}