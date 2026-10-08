using UnityEngine;
using UnityEngine.SceneManagement;

public class title_screen : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject infoScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void onPlayButtonPressed()
    {
        SceneManager.LoadScene("Main");
    }

    public void onCreditsButtonPressed()
    {
        if (infoScreen == null) {return;}

        infoScreen.SetActive(true);
    }

    public void onCreditsBackButtonPressed()
    {
        if (infoScreen == null) {return;}

        infoScreen.SetActive(false);
    }

    public void onSettingsButtonPressed()
    {
        if (settingsMenu == null) {return;}

        settingsMenu.SetActive(true);
    }

    public void onExitButtonPressed()
    {
        Application.Quit();
    }
}
