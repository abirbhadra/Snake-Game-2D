using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject uiImage;

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
}
