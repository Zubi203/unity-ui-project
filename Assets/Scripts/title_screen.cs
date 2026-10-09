using UnityEngine;


public class title_screen : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject infoScreen;
    [SerializeField] private LevelLoader sceneLoader;

    void Start()
    {
        CustomCursorManager.Instance.SetCursor(Resources.Load<Texture2D>("CustomCursor"));
    }

    public void onPlayButtonPressed()
    {
        sceneLoader.TransitionToScene("Main");
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
