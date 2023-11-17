using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuImage;

    // Function to enable the image UI object
    public void EnableImage()
    {
        PauseMenuImage.SetActive(true);
    }

    // Function to disable the image UI object
    public void DisableImage()
    {
        PauseMenuImage.SetActive(false);
    }
}
