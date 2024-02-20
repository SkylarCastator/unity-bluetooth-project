using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlueoothUI : MonoBehaviour
{
    public BluetoothController bluetoothController;
    public GameObject connectionScreen;
    public TMP_Text connectionLabel;
    public TMP_InputField searchDeviceName;
    public GameObject searchContainer;
    public GameObject bluetoothDeviceButton;
    
    public GameObject controllerScreen;
    public TMP_Text deviceName;
    public TMP_Text ledStateLabel;
    public Sprite ledOnSprite;
    public Sprite ledOffSprite;
    public Image ledStateImage;
    public bool ledOn;


    public void Start()
    {
        controllerScreen.SetActive(false);
        connectionScreen.SetActive(true);
        ledStateLabel.text = "LED OFF";
        ledStateImage.sprite = ledOffSprite;
    }

    public void ConnectButtonPressed()
    {
        if (bluetoothController != null)
        {
            bool connected = bluetoothController.ConnectDevice(searchDeviceName.text);
            if (connected)
            {
                EnableControllerScreen();
            }
        }
        else
        {
            Debug.LogError("Bluetooth Controller is null");
        }
    }

    public void SearchDevices()
    {
        if (bluetoothController != null)
        {
            string[] devices = bluetoothController.GetBluetoothDevices();
            PopulateSearchContainer(devices);
        }
    }

    public void DisconnectButtonPressed()
    {
        if (bluetoothController != null)
        {
            controllerScreen.SetActive(false);
            connectionScreen.SetActive(true);
            bluetoothController.DisconnectDevice();
        }
        else
        {
            Debug.LogError("Bluetooth Controller is null");
        }
    }

    private void EnableControllerScreen()
    {
        controllerScreen.SetActive(true);
        connectionScreen.SetActive(false);
    }

    public void LEDButtonPressed()
    {
        if (ledOn)
        {
            ledStateLabel.text = "LED OFF";
            ledStateImage.sprite = ledOffSprite;
            bluetoothController.SendBluetoothMessage("LED OFF");
            ledOn = false;
        }
        else
        {
            ledStateLabel.text = "LED ON";
            ledStateImage.sprite = ledOnSprite;
            bluetoothController.SendBluetoothMessage("LED ON");
            ledOn = true;
        }      
    }

    private void PopulateSearchContainer(string[] bluetoothDevices)
    {
        foreach (Transform child in searchContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < bluetoothDevices.Length; i++)
        {
            GameObject button = GameObject.Instantiate(bluetoothDeviceButton, searchContainer.transform);
            DeviceButtonComponent buttonComponent = button.GetComponent<DeviceButtonComponent>();
            buttonComponent.buttonText.text = bluetoothDevices[i];
            buttonComponent.uiController = this;
        }    
    }
}
