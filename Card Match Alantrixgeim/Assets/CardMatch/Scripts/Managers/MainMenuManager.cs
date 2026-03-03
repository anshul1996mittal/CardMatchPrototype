using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages the UI and navigation for the main menu scene.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
   
    [SerializeField] private GameObject settingPanel;

    [SerializeField] private GameObject ExitPanel;

    private bool isExitPanelVisible;


    void Update()
    {
        // Handle the Escape key on desktop or the back button on Android to toggle the exit panel.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isExitPanelVisible = !isExitPanelVisible;
            ExitButton(isExitPanelVisible);
        }
    }

    /// <summary>
    /// Placeholder for a continue game feature. Currently not implemented.
    /// </summary>
    public void CountinueButton()
    {
        // TODO: Implement logic to continue a saved game.
    }
    
    /// <summary>
    /// Starts the game on Easy difficulty.
    /// Sets the grid size to 2x2 and loads the gameplay scene.
    /// </summary>
    public void EasyButton()
    {
        PlayerPrefsHalper.ROW_COUNT = 2;
        PlayerPrefsHalper.COLUMN_COUNT = 2;
        GameManager.Instance.SceneLoad("GamePlay");
    }
    /// <summary>
    /// Starts the game on Medium difficulty.
    /// Sets the grid size to 2x3 and loads the gameplay scene.
    /// </summary>
    public void MediumButton()
    {
        PlayerPrefsHalper.ROW_COUNT = 2;
        PlayerPrefsHalper.COLUMN_COUNT = 3;
        GameManager.Instance.SceneLoad("GamePlay");
    }
    /// <summary>
    /// Starts the game on Hard difficulty.
    /// Sets the grid size to 5x6 and loads the gameplay scene.
    /// </summary>
    public void HardButton()
    {
        PlayerPrefsHalper.ROW_COUNT = 5;
        PlayerPrefsHalper.COLUMN_COUNT = 6;
        GameManager.Instance.SceneLoad("GamePlay");
    }
    /// <summary>
    /// Shows or hides the settings panel.
    /// </summary>
    /// <param name="value">True to show, false to hide.</param>
    public void SettingButton(bool value)
    {
        settingPanel.SetActive(value);
    }
   /// <summary>
   /// Shows or hides the exit confirmation panel.
   /// </summary>
   /// <param name="value">True to show, false to hide.</param>
    public void ExitButton(bool value)
    {
        ExitPanel.SetActive(value);
    }
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
