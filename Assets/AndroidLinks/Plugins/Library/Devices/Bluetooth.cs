using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace AndroidLink
{
    public class Bluetooth
    {
        private AndroidLink.PluginInit pluginManager;
        private static AndroidJavaObject BluetoothConnector;
        

        public void InitalizeBluetoothConnector(AndroidJavaClass androidPlugin)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                
                BluetoothConnector = androidPlugin.CallStatic<AndroidJavaObject>("getInstance");
            }
        }

        public string[] SearchConnectedBluetoothDevices()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                try
                {
                    return BluetoothConnector.Call<string[]>("GetBluetoothDevices");
                }
                catch (Exception e)
                {
                    pluginManager.Toast("No Device found");
                    return null;
                }
            }

            return null;
        }

        public bool ConnectBluetoothDevice(string deviceName)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                try
                {
                    string status = BluetoothConnector.Call<string>("StartBluetoothConnection", deviceName);
                    pluginManager.Toast("Connecting Device : " + status);
                    if (status == "Connected") {return true;}                 
                }
                catch (Exception e)
                {
                    pluginManager.Toast("Start connection error");
                }
            }
            return false;
        }

        public bool DisconnectBluetoothDevice()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                try
                {
                    BluetoothConnector.Call("DisconnectBluetooth");
                    pluginManager.Toast("Disconnecting Device : ");
                }
                catch (Exception e)
                {
                    pluginManager.Toast("Start connection error");
                }
            }
            return false;
        }

        public void WritetoBluetooth(string data)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                try
                {
                    BluetoothConnector.Call("WriteData", data);
                }
                catch (Exception e)
                {
                    pluginManager.Toast("Write data error");
                }
            }
        }

        public string ReadFromBluetooth()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                try
                {
                    return BluetoothConnector.Call<string>("ReadData");
                }
                catch (Exception e)
                {
                    pluginManager.Toast("Could not read data");
                    //BluetoothConnector.Call("PrintOnScreen", pluginManager.context, "Read data error");
                }
            }

            return "";

        }
    }
}
