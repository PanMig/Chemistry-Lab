using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

namespace goedle_sdk.detail
{
    public class GoedleAnalytics
    {
        private string api_key = null;
        private string app_key = null;
        private string user_id = null;
        private string anonymous_id = null;
        private string app_version = null;
        private string ga_tracking_id = null;
        private string app_name = null;
        private int cd1;
        private int cd2;
        private string cd_event = null;
        private IGoedleHttpClient gio_http_client;
        private GoedleUtils goedleUtils = new GoedleUtils();

        //private string locale = null;

        public GoedleAnalytics(string api_key, string app_key, string user_id, string app_version, string ga_tracking_id, string app_name, int cd1, int cd2, string cd_event, IGoedleHttpClient gio_http_client)//, string locale)
        {
            this.api_key = api_key;
            this.app_key = app_key;
            this.user_id = user_id;
            this.app_version = app_version;
            this.ga_tracking_id = ga_tracking_id;
            this.app_name = app_name;
            this.cd1 = cd1;
            this.cd2 = cd2;
            this.cd_event = cd_event;
            this.gio_http_client = gio_http_client;
            //this.locale = GoedleLanguageMapping.GetLanguageCode (locale);
            track_launch();
        }

        public void reset_user_id(string user_id)
        {
            this.user_id = user_id;
        }

        public void set_user_id(string user_id)
        {
            this.anonymous_id = this.user_id;
            this.user_id = user_id;
            track(GoedleConstants.IDENTIFY, null, null, false, null, null);
        }

        public void track_launch()
        {
            track(GoedleConstants.EVENT_NAME_INIT, null, null, true, null, null);
        }

        public void track(string event_name, string event_id, string event_value, bool launch, string trait_key, string trait_value)
        {
            bool ga_active = !String.IsNullOrEmpty(this.ga_tracking_id);
            string authentication = null;
            string content = null;
            int ts = getTimeStamp();
            // -1 because c# returns -1 for UTC +1 , * 1000 from Seconds to Milliseconds
            int timezone = (int)(((DateTime.UtcNow - DateTime.Now).TotalSeconds) * -1 * 1000);
            GoedleAtom rt = new GoedleAtom(app_key, user_id, ts, event_name, event_id, event_value, timezone, app_version, anonymous_id, trait_key, trait_value, ga_active);
            if (rt == null)
            {
                Console.Write("Data Object is None, there must be an error in the SDK!");
            }
            else
            {
                content = rt.getGoedleAtomDictionary().ToString();
                authentication = goedleUtils.encodeToUrlParameter(content, api_key);
            }

            string url = GoedleConstants.TRACK_URL;
            gio_http_client.sendPost(url, content, authentication);

            // Sending tp Google Analytics for now we only support the Event tracking
            string type = "event";

            if (ga_active)
                trackGoogleAnalytics(event_name, event_id, event_value, type);
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
                sb.Append(UnityWebRequest.EscapeURL(item.Value.ToString()));
            }
            return sb.ToString();
        }

        public void trackGoogleAnalytics(string event_name, string event_id, string event_value, string type)
        {
            if (string.IsNullOrEmpty(event_name)) throw new ArgumentNullException();
            // the request body we want to send
            var postData = new Dictionary<string, string>
                           {
                               { "v", GoedleConstants.GOOGLE_MP_VERSION.ToString() },
                               {"av", app_version},
                                {"an", app_name},
                               { "tid", ga_tracking_id },
                                { "cid", user_id },
                               { "t", type },
                                { "ec", getSceneName() },
                               { "ea", event_name },
								//{"ul", this.locale},
                           };


            // This is the Event label in Google Analytics

            if (!String.IsNullOrEmpty(event_id))
            {
                postData.Add("el", event_id);
            }
            if (goedleUtils.IsFloatOrInt(event_value))
            {
                postData.Add("ev", event_value);
            }

            if (!String.IsNullOrEmpty(anonymous_id))
            {
                postData.Add("uid", user_id);
                // For mapping after identify
                // Otherwise we will lost the old client id
                postData.Remove("cid");
                postData.Add("cid", anonymous_id);
            }
            if (cd_event == "group" && event_name == "group" && cd1 != 0 && cd2 != 0 && cd1 != cd2)
            {
                postData.Remove("el");
                postData.Remove("ev");
                postData.Add(String.Concat("cd", cd1), event_id);
                postData.Add(String.Concat("cd", cd2), event_value);
            }

            gio_http_client.sendGet(buildGAUrlDataString(postData));
        }


        public void track(string event_name)
        {
            track(event_name, null, null, false, null, null);
        }


        public void track(string event_name, string event_id)
        {
            track(event_name, event_id, null, false, null, null);
        }

        public void track(string event_name, int event_id_i)
        {
            string event_id = event_id_i.ToString();

            track(event_name, event_id, null, false, null, null);
        }

        public void track(string event_name, string event_id, string event_value)
        {
            track(event_name, event_id, event_value, false, null, null);

        }

        public void trackGroup(string group_type, string group_member)
        {
            track("group", group_type, group_member, false, null, null);
        }

        public void trackTraits(string trait_key, string trait_value)
        {
            track(GoedleConstants.IDENTIFY, null, null, false, trait_key, trait_value);
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