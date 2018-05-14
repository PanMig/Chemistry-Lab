using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


namespace goedle_sdk.detail {

    public interface IGoedleDownloadBuffer
    {
        DownloadHandler downloadHandlerBuffer { get; }
        string text { get; }
    }

    public class GoedleDownloadBuffer : IGoedleDownloadBuffer
    {

        DownloadHandler _downloadHandlerBuffer { get; set; }

        public GoedleDownloadBuffer()
        {
            _downloadHandlerBuffer =(DownloadHandler) new DownloadHandlerBuffer();
        }

        public DownloadHandler downloadHandlerBuffer
        {
            get { return _downloadHandlerBuffer; }
        }

        public string text
        {
            get { return _downloadHandlerBuffer.text; }
        }

    }
}

