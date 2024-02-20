using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;


public class BluetoothController : MonoBehaviour
{
    private string connectedDeviceName = "";
    private bool deviceConnected = false;
    private string dataRecived = "";

    void Start()
    {
#if UNITY_2020_2_OR_NEWER
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
          || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
            Permission.RequestUserPermissions(new string[] {
                        Permission.CoarseLocation,
                            Permission.FineLocation,
                            "android.permission.BLUETOOTH_SCAN",
                            "android.permission.BLUETOOTH_ADVERTISE",
                             "android.permission.BLUETOOTH_CONNECT"
                    });
        #endif
    #endif
        deviceConnected = false;
        BluetoothService.CreateBluetoothObject();
    }

    void Update()
    {
        if (deviceConnected)
        {
            try
            {
                string datain = BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                    print(dataRecived);
                }

            }
            catch (Exception e)
            {
                BluetoothService.Toast("Error in connection");
            }
        }

    }

    public string[] GetBluetoothDevices()
    {
        return BluetoothService.GetBluetoothDevices(); ;
    }

    public bool ConnectDevice(string deviceName)
    {
        if (!deviceConnected)
        {
            deviceConnected = BluetoothService.StartBluetoothConnection(deviceName);
            BluetoothService.Toast(deviceName + " status: " + deviceConnected);
        }
        return deviceConnected;
    }

    public bool DisconnectDevice()
    {
        if (deviceConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
        return deviceConnected;
    }

    public void SendBluetoothMessage(string message)
    {
        if (deviceConnected && (message != "" || message != null))
            BluetoothService.WritetoBluetooth(message);
        else
            BluetoothService.WritetoBluetooth("Not connected");
    }
}
