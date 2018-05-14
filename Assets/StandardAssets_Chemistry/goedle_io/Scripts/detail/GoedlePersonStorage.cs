using System;

namespace goedle_sdk.detail
{
	public class GoedlePersonStorage
	{
		public string user_id = null;
        public string email = null;
        public string first_name = null;
        public string surname = null;
        public string group_id = null;
		public bool new_person = true;

		public GoedlePersonStorage(bool temp_user){
			build_person(temp_user);
		}

		public void build_person(bool temp_user) {
			if (temp_user == true){
					this.user_id = create_user_id();
					new_person = false;
					}
			else{
				if (new_person == true)
					this.user_id = create_user_id();
					new_person = false;
					}

		}
		public string create_user_id(){
			Guid GUID = System.Guid.NewGuid();
			return GUID.ToString();
		}

		public string getUserId(){
			return this.user_id;
	}
}
}

