using UnityEngine.Networking;
using System.Text;

namespace goedle_sdk.detail
{
    public interface IGoedleUploadHandler
    {
        UploadHandler uploadHandler { get; set; }
        void add(string stringContent);
    }

    public class GoedleUploadHandler : IGoedleUploadHandler
    {
        UploadHandler _uploadHandler { get; set; }


        public void add (string stringContent){
            byte[] byteContentRaw = new UTF8Encoding().GetBytes(stringContent);
            _uploadHandler = (UploadHandler)new UploadHandlerRaw(byteContentRaw);
        }

        public UploadHandler uploadHandler
        {
            get { return _uploadHandler; }
            set { _uploadHandler = value; }
        }
    }
}