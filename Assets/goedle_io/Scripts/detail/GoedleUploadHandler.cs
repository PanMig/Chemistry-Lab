using UnityEngine.Networking;
using System.Text;

namespace goedle_sdk.detail
{
    public interface IGoedleUploadHandler
    {
        UploadHandler uploadHandler { get; set; }
        void add(string stringContent);
        string getDataString();
    }

    public class GoedleUploadHandler : IGoedleUploadHandler
    {
        UploadHandler _uploadHandler { get; set; }


        public void add (string stringContent){
            byte[] byteContentRaw = new UTF8Encoding().GetBytes(stringContent);
            _uploadHandler = (UploadHandler)new UploadHandlerRaw(byteContentRaw);
        }

        public string getDataString()
        { 
            return Encoding.UTF8.GetString(_uploadHandler.data);
        }

        public UploadHandler uploadHandler
        {
            get { return _uploadHandler; }
            set { _uploadHandler = value; }
        }
    }
}