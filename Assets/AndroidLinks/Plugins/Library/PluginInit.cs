using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndroidLink
{
    public class PluginInit
    {
        private static AndroidJavaClass unityClass;
        private static AndroidJavaObject unityActivity;
        private static AndroidJavaObject _pluginInstance;
        public static AndroidJavaObject context;

        private void Start()
        {
            InitializePlugin("com.voidstudio.unityplugin.PluginInstance");
        }

        void InitializePlugin(string pluginName)
        {
            unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            unityActivity = unityClass.GetStatic<AndroidJavaObject>("currectActivity");
            _pluginInstance = new AndroidJavaObject(pluginName);
            context = unityActivity.Call<AndroidJavaObject>("getApplicationContext");
            if (_pluginInstance != null)
            {
                Debug.Log("Plugin Instance Error");
            }
            _pluginInstance.CallStatic("recieveUnityActivity", unityActivity);
        }

        public void Add()
        {
            if (_pluginInstance != null)
            {
                var result = _pluginInstance.Call<int>("Add", 5, 6);
                Debug.Log("Add result from unity: " + result);
            }
        }

        public void Toast(string message)
        {
            if (_pluginInstance == null)
            {
                _pluginInstance.Call("Toast", message);
            }
        }
    }
}