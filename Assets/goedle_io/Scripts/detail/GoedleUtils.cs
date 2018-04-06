using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

namespace goedle_sdk.detail
{
    public interface IGoedleUtils{
        string HexStringFromBytes(byte[] bytes);
        string getStrategyUrl(string url);
        string encodeToUrlParameter(string content, string api_key);
        bool IsFloatOrInt(string value);
    }

    public class GoedleUtils : IGoedleUtils
    {
        public GoedleUtils(){
            
        }

        public static string userHash(string strToEncrypt)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);
            // encrypt bytes
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);
            Guid user_id_hash = new Guid(hashBytes);
            return user_id_hash.ToString();
        }

        public string HexStringFromBytes(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public string encodeToUrlParameter(string content, string api_key)
        {
            byte[] authData = Encoding.UTF8.GetBytes(content + api_key);
            SHA1 sha = new SHA1CryptoServiceProvider();
            string hashedAuthData = HexStringFromBytes((sha.ComputeHash(authData)));
            return hashedAuthData;
        }

        public bool IsFloatOrInt(string value)
        {
            int intValue;
            float floatValue;
            return Int32.TryParse(value, out intValue) || float.TryParse(value, out floatValue);
        }
        public string getStrategyUrl(string app_key)
        {
            // TODO: build strategy url
            return GoedleConstants.STRATEGY_URL + app_key + GoedleConstants.STRATEGY_PATH;
        }
    }
    public static class UriHelper
    {
        public static Dictionary<string, string> DecodeQueryParameters(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (uri.Query.Length == 0)
                return new Dictionary<string, string>();
            return uri.Query.TrimStart('?')
                            .Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                            .GroupBy(parts => parts[0], parts => parts.Length > 2 ? string.Join("=", parts, 1, parts.Length - 1) : (parts.Length > 1 ? parts[1] : ""))
                      .ToDictionary(grouping => grouping.Key, grouping => string.Join(",", grouping.ToArray()));
        }
    }
    public class CoroutineWithData : MonoBehaviour
    {
        public Coroutine coroutine { get; private set; }
        public object result;
        private IEnumerator target;
        public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
        {
            this.target = target;
            this.coroutine = owner.StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            while (target.MoveNext())
            {
                result = target.Current;
                yield return result;
            }
        }
    }
}