using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button SinglePlayer;
    public Button COoP;
    public Button Guide;
    public Button Exit;

    public GuideUI guideUI;
    private void Start()
    {
        SinglePlayer.onClick.AddListener(SinglePlayerButton);
        COoP.onClick.AddListener(CoopButtonButton);
        Guide.onClick.AddListener(GuideButton);
        //Exit.onClick.AddListener(ExitButton);

    }

    public void SinglePlayerButton()
    {
        int SceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        SceneManager.LoadScene(SceneIndex);
    }
    public void CoopButtonButton()
    {
        int SceneIndex = SceneManager.GetActiveScene().buildIndex + 2;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneIndex);

    }
    public void GuideButton()
    {
        guideUI.EnableImage();

    }
}
