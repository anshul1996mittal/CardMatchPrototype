using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Manages the gameplay UI, score, timer, and game states (Pause, Level Complete, Level Failed).
/// </summary>
public class GamePlayManager : Singleton<GamePlayManager>
{
    [SerializeField] private GameSave gameSave;
    [Header("Top Panel Objects")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text timerTxt;

    [Header("Combo Components")]
    [SerializeField] private GameObject comboObject;
    [SerializeField] private Image comboFillImage;
    [SerializeField] private Text comboTxt;

    [Header("Level Completed")]
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Text levelCompleteCurrectScoreTxt;
    [SerializeField] private Text levelCompleteHighScoreTxt;
    
    [Header("Level Failed")]
    [SerializeField] private GameObject levelFailedPanel;
    [SerializeField] private Text levelFailedHighScoreTxt;
    [SerializeField] private AudioClip levelFailedClip, levelCompleteClip;
    
    [Header("Other")]
    public ScoreEffect scoreEffect;

    /// <summary>
    /// Updates the score text UI.
    /// </summary>
    public void ScoreUpdate(int score)
    {
        scoreTxt.text = score+"";
    }

    /// <summary>
    /// Updates the timer text UI.
    /// </summary>
    public void TimerUpdate(string timer)
    {
        timerTxt.text = timer + "";
    }

    /// <summary>
    /// Updates the combo UI (text and fill image).
    /// </summary>
    public void ComboTimerUpdate(int comboCount,float timerFillAmount)
    {
       ComboObjectActivate(true);
        comboTxt.text = (comboCount ) + "X";
        comboFillImage.fillAmount = timerFillAmount;
    }

    /// <summary>
    /// Activates or deactivates the combo object.
    /// </summary>
    public void ComboObjectActivate(bool value)
    {
        comboObject.SetActive(value);
    }

    /// <summary>
    /// Handles level completion logic: shows panel, updates high score, plays sound.
    /// </summary>
    public void LevelCompleted(int currentScore)
    {
        levelCompletePanel.SetActive(true);
        levelCompleteCurrectScoreTxt.text = currentScore + "";
        if (currentScore > PlayerPrefsHalper.HIGH_SCORE)
        {
            PlayerPrefsHalper.HIGH_SCORE = currentScore;
        }
        levelCompleteHighScoreTxt.text = PlayerPrefsHalper.HIGH_SCORE+"";
        PlayerPrefsHalper.IS_GAME_SAVED = false;
        Events.playAudio(levelCompleteClip);
    }

    /// <summary>
    /// Handles level failure logic: shows panel, plays sound.
    /// </summary>
    public void LevelFailed()
    {
        levelFailedPanel.SetActive(true);
        levelFailedHighScoreTxt.text = PlayerPrefsHalper.HIGH_SCORE + "";
        PlayerPrefsHalper.IS_GAME_SAVED = false;
        Events.playAudio(levelFailedClip);
    }
#region Button Listener 
    /// <summary>
    /// Pauses or unpauses the game.
    /// </summary>
    public void PauseButton(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseScreen.SetActive(value);
    }

    /// <summary>
    /// Saves the game and loads the Main Menu.
    /// </summary>
    public void ExitGame()
    {
        Time.timeScale = 1;
        gameSave.SavedGame();
        GameManager.Instance.SceneLoad("MainMenu");
    }

    /// <summary>
    /// Restarts the game (resets save flag and reloads scene).
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1;
        PlayerPrefsHalper.IS_GAME_SAVED = false;
        GameManager.Instance.SceneLoad("GamePlay");
    }

    /// <summary>
    /// Loads the Main Menu directly.
    /// </summary>
    public void OpenMainMenu()
    {
        GameManager.Instance.SceneLoad("MainMenu");
    }
    #endregion
}
