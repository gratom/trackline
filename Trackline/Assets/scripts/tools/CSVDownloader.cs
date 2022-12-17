using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Tools
{
    public static class CSVDownloader
    {
        private const string url = "https://docs.google.com/spreadsheets/d/";
        private const string command = "/export?format=csv";

        public static void Download(string docID, Action<string> onComleteCallback)
        {
            UnityWebRequest req = UnityWebRequest.Get(url + docID + command);
            req.SendWebRequest().completed += (x) =>
            {
                if (req.isNetworkError)
                {
                    Debug.LogError("Network error.");
                }
                else
                {
                    Debug.Log("Download success\n" + req.downloadHandler.text);
                    onComleteCallback?.Invoke(req.downloadHandler.text);
                }
            };
        }
    }
}
