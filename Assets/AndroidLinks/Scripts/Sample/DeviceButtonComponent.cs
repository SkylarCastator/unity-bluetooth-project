using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceButtonComponent : MonoBehaviour
{
    public TMP_Text buttonText;
    public BlueoothUI uiController;

    public void ButtonClicked()
    {
        uiController.searchDeviceName.text = buttonText.text;
        uiController.ConnectButtonPressed();
    }
}
