using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUI : MonoBehaviour
{
    public GameObject GuideImage;

    // Function to enable the image UI object
    public void EnableImage()
    {
        GuideImage.SetActive(true);
    }

    // Function to disable the image UI object
    public void DisableImage()
    {
        GuideImage.SetActive(false);
    }
}
