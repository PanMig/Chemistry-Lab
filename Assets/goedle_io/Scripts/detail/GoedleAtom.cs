using System;
using System.Collections.Generic;
using SimpleJSON;

namespace goedle_sdk.detail
{
	public class GoedleAtom
	{
		/* NOT YET IMPLEMENTED
		 *
		private string dev_version;
		private string screen = null;
		private string device_type = null;
		private string locale;
		*/

		private string app_key;
		private string user_id;
		private int ts;
		private string event_name = null;
		private string event_value = null;
		private int timezone;
		private string event_id = null;
		private string build_nr = GoedleConstants.BUILD_NR;
		private string trait_key = null;
		private string trait_value = null;
		private string app_version = null;
		private string anonymous_id = null;
		private string uuid = null;

		public GoedleAtom (string app_key, 
		                  string user_id, 
						  int ts, 
		                  string event_name, 
		                  string event_id, 
		                  string event_value,
		                  int timezone,
		                  string app_version,
						  string anonymous_id,
		                  string trait_key, 
						  string trait_value,
						  bool ga_active)
		{
			this.app_key = app_key;
			if (user_id == null) {
				Console.Write ("Maybe the GoedleAPI.init(); isn`t called in the Application class. Or you don't have set the GoedleAPI.setUserId(userId)?");
			}else
			{
				this.user_id = user_id;
			}			
			this.ts = ts;
			this.event_name = event_name;
			if (!string.IsNullOrEmpty (event_id))
				this.event_id = event_id;
			if (!string.IsNullOrEmpty (event_value))
				this.event_value = event_value;
			if (!string.IsNullOrEmpty (trait_key))
				this.trait_key = trait_key;
			if (!string.IsNullOrEmpty (trait_value))
				this.trait_value = trait_value;
			if (!string.IsNullOrEmpty (anonymous_id))
				this.anonymous_id = anonymous_id;
				// This is for the google analytics case
				if (ga_active)	
					this.uuid = anonymous_id;
			this.timezone = timezone;
			this.app_version = app_version;

		}

        public JSONObject getGoedleAtomDictionary ()
		{
            var goedleAtom = new JSONObject();
            goedleAtom.Add ("app_key", this.app_key);
			goedleAtom.Add ("user_id", this.user_id);
			goedleAtom.Add ("ts", this.ts);
			goedleAtom.Add ("event", this.event_name);
			goedleAtom.Add ("build_nr", build_nr);
			goedleAtom.Add ("app_version", this.app_version);
			goedleAtom.Add ("timezone", this.timezone);
			/*if (!string.IsNullOrEmpty (this.locale))
				goedleAtom.Add ("locale", this.locale);*/
			if (!string.IsNullOrEmpty (this.anonymous_id))
				goedleAtom.Add ("anonymous_id", this.anonymous_id);
			if (!string.IsNullOrEmpty (this.uuid))
				goedleAtom.Add ("uuid", this.uuid);
			if (!string.IsNullOrEmpty (this.event_id))
				goedleAtom.Add ("event_id", this.event_id);
			if (!string.IsNullOrEmpty (this.event_value))
				goedleAtom.Add ("event_value", this.event_value);
	
			if (!string.IsNullOrEmpty (this.trait_key) && !string.IsNullOrEmpty (this.trait_value))
				goedleAtom.Add (this.trait_key, this.trait_value);
			return goedleAtom;
		}
	}
}
