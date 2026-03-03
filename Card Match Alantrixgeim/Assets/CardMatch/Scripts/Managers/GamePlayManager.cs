using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public GameObject pauseScreen;
    public Text scoreTxt;
    public Text timerTxt;

    [Header("Combo Components")]
    public GameObject comboObject;
    public Image comboFillImage;
    public Text comboTxt;

    public void ScoreUpdate(int score)
    {
        scoreTxt.text = score+"";
    }

    public void TimerUpdate(int timer)
    {
        timerTxt.text = timer + "";
    }

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
    public void ExitGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.SceneLoad("MainMenu");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        GameManager.Instance.SceneLoad("GamePlay");
    }
}
