using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerUI : MonoBehaviour
{
    public GameObject uiImage;

    public GameObject GreenWinnerUI;

    public GameObject RedWinnerUI;
    // Function to enable the image UI object
    public void EnableImage()
    {
        uiImage.SetActive(true);
    }

    // Function to disable the image UI object
    public void DisableImage()
    {
        uiImage.SetActive(false);
    }
    public void EnableImageRed()
    {
        RedWinnerUI.SetActive(true);
    }

    // Function to disable the image UI object
    public void DisableImageRed()
    {
        RedWinnerUI.SetActive(false);
    }
    public void EnableImageGreen()
    {
        GreenWinnerUI.SetActive(true);
    }

    // Function to disable the image UI object
    public void DisableImageGreen()
    {
        GreenWinnerUI.SetActive(false);
    }
}
