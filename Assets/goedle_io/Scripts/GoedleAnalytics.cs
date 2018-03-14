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
        [Tooltip("The APP Key of the goedle.io project.")]
        public string app_key = "";
        [Tooltip("The API Key of the goedle.io project.")]
        public string api_key = "";
		[Tooltip("Enable (True)/ Disable (False) tracking with goedle.io, default is True")]
		public bool ENABLE_GOEDLE = true;
		[Tooltip("You can specify an app version here.")]
		public string APP_VERSION = "";
		[Tooltip("You should specify an app name here.")]
		public string APP_NAME = "";
        [Tooltip("Enable (True) / Disable(False) additional tracking with Google Analytics")]
        public bool ENABLE_GA = true;
        [Tooltip("Google Analytics Tracking Id")]
        public string GA_TRACKIND_ID = null;
        [Tooltip("Google Analytics Custom Dimension Event Listener. This is for group call support.")]
        public string GA_CD_EVENT = null;
        [Tooltip("Google Analytics Number of Custom Dimension for Group type. (To set this you need a configured custom dimension in Google Analytics)")]
        public int GA_CD_1 = 0;
        [Tooltip("Google Analytics Number of Custom Dimension for Group member. (To set this you need a configured custom dimension in Google Analytics)")]
        public int GA_CD_2 = 0;
        #endregion
        /*! \endcond */




        /// <summary>
        /// Tracks an event.
        /// </summary>
        /// <param name="event">the name of the event to send</param>
        public static void track(string eventName)
        {
			#if !ENABLE_GOEDLE
                goedle_analytics.track(eventName);
            #endif
        }

		/// <summary>
		/// Tracks an event.
		/// </summary>
		/// <param name="event">the name of the event to send</param>
		/// <param name="event_id">the name of the event to send</param>

		public static void track(string eventName, string eventId)
		{
			#if !ENABLE_GOEDLE
                goedle_analytics.track(eventName,eventId);
			#endif
		}

		/// <summary>
		/// Tracks an event.
		/// </summary>
		/// <param name="event">the name of the event to send</param>
		/// <param name="event_id">the id of the event to send</param>
		/// <param name="event_value">the value of the event to send</param>

		public static void track(string eventName, string eventId, string event_value)
		{
			#if !ENABLE_GOEDLE
                goedle_analytics.track(eventName,eventId,event_value);
			#endif
		}


		/// <summary>
		/// Identify function for a user.
		/// </summary>
		/// <param name="trait_key">for now only last_name and first_name is supported</param>
		/// <param name="trait_value">the value of the key</param>

		public static void trackTraits(string traitKey, string traitValue)
		{
			#if !ENABLE_GOEDLE
                goedle_analytics.trackTraits(null, null, null, traitKey, traitValue);
			#endif
		}


		/// <summary>
		/// Group tracking function for a user.
		/// </summary>
		/// <param name="group_type">The entity type, like school or company</param>
		/// <param name="group_member">The name or identifier for the entity, like department number, class number</param>

		public static void trackGroup(string group_type, string group_member)
		{
			#if !ENABLE_GOEDLE
                goedle_analytics.trackGroup(group_type, group_member);
			#endif
		}

		/// <summary>
		/// set user id function for a user.
		/// </summary>
		/// <param name="user_id">a custom user id</param>

		public static void setUserId(string user_id)
		{
			#if !ENABLE_GOEDLE
                goedle_analytics.set_user_id(user_id);
			#endif
		}


        /// <summary>
        /// returns the strategies from the goedle api
        /// </summary>

        public static JSONNode getStrategy()
        {
        #if !ENABLE_GOEDLE
            return goedle_analytics.getStrategy();
        #endif
        }


		#region internal
        public static detail.GoedleAnalytics gio_interface;
        public static detail.GoedleAnalytics goedle_analytics
		{
			get
			{
				return gio_interface;
			}
		}

        static bool tracking_enabled = true;

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
            InitGoedle();
        }

		private void InitGoedle()
		{
            #if ENABLE_GOEDLE
            tracking_enabled = false;
            test_flag = false;
            Debug.LogWarning("Your Unity version does not support native plugins. Disabling goedle.io.");
            #endif
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

            //string locale = Application.systemLanguage.ToString();
            // Build HTTP CLient

            IGoedleHttpClient gio_http_client = new GoedleHttpClient(this);
            if (tracking_enabled && gio_interface == null)
            {
                gio_interface = new detail.GoedleAnalytics(api_key, app_key, user_id.ToString("D"), app_version, GA_TRACKIND_ID, app_name, GA_CD_1, GA_CD_2, GA_CD_EVENT, gio_http_client);
            }
		}

		void OnDestroy()
        {

			// Future Usage
        }

		#endregion

    }

}


