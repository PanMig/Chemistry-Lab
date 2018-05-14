/*
    *** do not modify the line below, it is updated by the build scripts ***
    goedle.io SDK for Unity version v1.0.0
*/

/*
#if !UNITY_PRO_LICENSE && (UNITY_2_6||UNITY_2_6_1||UNITY_3_0||UNITY_3_0_0||UNITY_3_1||UNITY_3_2||UNITY_3_3||UNITY_3_4||UNITY_3_5||UNITY_4_0||UNITY_4_0_1||UNITY_4_1||UNITY_4_2||UNITY_4_3||UNITY_4_5||UNITY_4_6)
#define DISABLE_GOEDLE
#warning "Your Unity version does not support native plugins - goedle.io disabled"
#endif
*/

using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Networking;
namespace goedle_sdk
{

    /// <summary>
    /// Core class for interacting with %goedle.io .
    /// </summary>
    /// <description>
    /// <p>Create a GameObject and attach this %goedle.io component. Then, set the properties in the unity inspector (app_key, api_key)</p>
    /// <p>Use the GoedleAnalytics class to set up your project and track events with %goedle.io . Once you have
    /// a component, you can track events in %goedle.io Engagement using <c>GoedleAnalytics.track(string eventName)</c>.
    /// </description>

    public class GoedleAnalytics : MonoBehaviour
    {
        public static GoedleAnalytics instance = null;
        /*! \cond PRIVATE */
        #region settings
        [Header("Project")]
        [Tooltip("Enable (True) / Disable(False) stageing_enviroment, highly recommeded for testing")]
        public bool staging = false;
        [Tooltip("The APP Key of the goedle.io project.")]
        public string app_key = "";
        [Tooltip("The API Key of the goedle.io project.")]
        public string api_key = "";
		[Tooltip("You can specify an app version here.")]
		public string APP_VERSION = "";
		[Tooltip("You should specify an app name here.")]
		public string APP_NAME = "";
        [Tooltip("Google Analytics Tracking Id")]
        public string GA_TRACKIND_ID = null;
        [Tooltip("Google Analytics Custom Dimension Event Listener. This is for group call support.")]
        public string GA_CD_EVENT = null;
        [Tooltip("Google Analytics Number of Custom Dimension for Group type. (To set this you need a configured custom dimension in Google Analytics)")]
        public int GA_CD_1 = 0;
        [Tooltip("Google Analytics Number of Custom Dimension for Group member. (To set this you need a configured custom dimension in Google Analytics)")]
        public int GA_CD_2 = 0;
        [Tooltip("Enable (True) / Disable(False) only content adaptation")]
        public bool adaptation_only = false;
        #endregion
        /*! \endcond */

        /// <summary>
        /// Tracks an event.
        /// </summary>
        /// <param name="event">the name of the event to send</param>
        public void track(string eventName)
        {
            goedle_analytics.track(eventName, new detail.GoedleUploadHandler());
        }

		/// <summary>
		/// Tracks an event.
		/// </summary>
		/// <param name="event">the name of the event to send</param>
		/// <param name="event_id">the name of the event to send</param>

		public void track(string eventName, string eventId)
		{
            goedle_analytics.track(eventName,eventId, new detail.GoedleUploadHandler());
		}

		/// <summary>
		/// Tracks an event.
		/// </summary>
		/// <param name="event">the name of the event to send</param>
		/// <param name="event_id">the id of the event to send</param>
		/// <param name="event_value">the value of the event to send</param>

		public void track(string eventName, string eventId, string event_value)
		{
            goedle_analytics.track(eventName,eventId,event_value, new detail.GoedleUploadHandler());
		}

		/// <summary>
		/// Identify function for a user.
		/// </summary>
		/// <param name="trait_key">for now only last_name and first_name is supported</param>
		/// <param name="trait_value">the value of the key</param>

		public void trackTraits(string traitKey, string traitValue)
		{
            goedle_analytics.trackTraits(traitKey, traitValue, new detail.GoedleUploadHandler());
		}

		/// <summary>
		/// Group tracking function for a user.
		/// </summary>
		/// <param name="group_type">The entity type, like school or company</param>
		/// <param name="group_member">The name or identifier for the entity, like department number, class number</param>

		public void trackGroup(string group_type, string group_member)
		{
            goedle_analytics.trackGroup(group_type, group_member, new detail.GoedleUploadHandler());
		}

		/// <summary>
		/// set user id function for a user.
		/// </summary>
		/// <param name="user_id">a custom user id</param>

		public void setUserId(string user_id)
		{
            goedle_analytics.set_user_id(user_id, new detail.GoedleUploadHandler());
		}


        /// <summary>
        /// request strategy from GIO API
        /// </summary>s
        public void requestStrategy() 
        {
            if (!staging)
            {
                detail.IGoedleDownloadBuffer goedleDownloadBuffer = new detail.GoedleDownloadBuffer();
                goedle_analytics.requestStrategy(goedleDownloadBuffer);
            }
        }

        /// <summary>
        /// Reset user id
        /// </summary>

        public void resetUserId()
        {
            Guid new_user_id = Guid.NewGuid();
            goedle_analytics.reset_user_id(new_user_id.ToString("D"));
        }

		#region internal
        public detail.GoedleAnalytics gio_interface;
        public detail.GoedleAnalytics goedle_analytics
		{
			get
			{
				return gio_interface;
			}
		}

        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
            {
                //if not, set instance to this
                instance = this;
                InitGoedle();
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

		private void InitGoedle()
		{
            Guid user_id = Guid.NewGuid();
            string app_version = APP_VERSION;
            string app_name = APP_NAME;
            if (String.IsNullOrEmpty(app_version))
                app_version = Application.version;
            if (String.IsNullOrEmpty(app_name))
                if (String.IsNullOrEmpty(app_name))
                    app_name = Application.productName;
                else
                    app_name = app_version;
            // string locale = Application.systemLanguage.ToString();
            // Build HTTP CLient
            detail.IGoedleWebRequest goedleWebRequest = new detail.GoedleWebRequest();
            detail.IGoedleUploadHandler goedleUploadHandler = new detail.GoedleUploadHandler();
            if (gio_interface == null && (!string.IsNullOrEmpty(instance.api_key) || !string.IsNullOrEmpty(instance.app_key)))
            {
                gio_interface = new detail.GoedleAnalytics(api_key, app_key, user_id.ToString("D"), app_version, GA_TRACKIND_ID, app_name, GA_CD_1, GA_CD_2, GA_CD_EVENT, detail.GoedleHttpClient.instance, goedleWebRequest, goedleUploadHandler, staging, adaptation_only);
                Debug.Log("goedle.io SDK is initialzied");
            }
		}

		void OnDestroy()
        {

			// Future Usage
        }

		#endregion

    }

}


