using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngineInternal;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine;
using UnityEngine.Networking;

namespace goedle_sdk.detail {

    public interface IGoedleWebRequest
    {
        bool isHttpError { get;}
        bool isNetworkError { get; }
        string url { get; set; }
        long responseCode { get; }
        string method { get; set; }
        bool chunkedTransfer{ get; set; }
        DownloadHandler downloadHandler { get; set; }
        UploadHandler uploadHandler { get; set; }
        UnityWebRequest unityWebRequest { get; set; }
        UnityWebRequestAsyncOperation SendWebRequest();
        void SetRequestHeader(string name, string value);
    }

    public class GoedleWebRequest : IGoedleWebRequest{

        UnityWebRequest _unityWebRequest { get; set; }
       
        public GoedleWebRequest(){
            
        }

        public bool isNetworkError
        {
            get { return _unityWebRequest.isNetworkError; }
        }

        public bool isHttpError
        {
            get { return _unityWebRequest.isHttpError; }
        }

        public string url
        {
            get { return _unityWebRequest.url; }
            set { _unityWebRequest.url = value; }
        }

        public string method
        {
            get { return _unityWebRequest.method; }
            set { _unityWebRequest.method = value; }
        }

        public bool chunkedTransfer
        {
            get { return _unityWebRequest.chunkedTransfer; }
            set { _unityWebRequest.chunkedTransfer = value; }
        }

        public UnityWebRequest unityWebRequest
        {
            get { return _unityWebRequest; }
            set { _unityWebRequest = value; }

        }

        public DownloadHandler downloadHandler 
        { 
            get { return _unityWebRequest.downloadHandler;  }
            set { _unityWebRequest.downloadHandler = value;  }
        }

        public UploadHandler uploadHandler
        {
            get { return _unityWebRequest.uploadHandler; }
            set { _unityWebRequest.uploadHandler = value; }
        }

        public UnityWebRequestAsyncOperation SendWebRequest()
        {
            return _unityWebRequest.SendWebRequest();
        }

        public long responseCode
        {
            get { return _unityWebRequest.responseCode; }
        }

        public void SetRequestHeader(string name, string value)
        {
            _unityWebRequest.SetRequestHeader(name,value);
        }
    }
}