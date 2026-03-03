using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles saving and loading the game state using PlayerPrefs.
/// Manages persistence of game timer, score, and card states (flipped/matched).
/// </summary>
public class GameSave : MonoBehaviour
{
    public MemoryGameController gameController;

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!gameController.isGameCompleted)
            {
                SavedGame();
            }
        }
    }
    private void OnApplicationQuit()
    {
        if (!gameController.isGameCompleted)
        {
            SavedGame();
        }
    }

    /// <summary>
    /// Saves the current game state (timer, score, card layout, flipped status) to PlayerPrefs.
    /// </summary>
    public void SavedGame()
    {
        PlayerPrefsHalper.IS_GAME_SAVED = true;
        PlayerPrefsHalper.SAVED_GAME_TIMER = gameController.currentTimer;
        PlayerPrefsHalper.SAVED_GAME_CURRENT_SCORE = gameController.currentScore;
        PlayerPrefsHalper.COUNT_CORRECT_CARD_FLIP = gameController.countCorrectCardFlip;
        List<string> cardData = new List<string>();
        for(int i=0;i<gameController.suffledSpriteList.Count;i++){
            cardData.Add(gameController.suffledSpriteList[i].name);
        }
        PlayerPrefsHalper.SAVED_GAME_CARD_DATA = cardData;
        PlayerPrefsHalper.SAVED_GAME_CARD_FLIPPED_DATA = gameController.CardFlippedData;
    }

    /// <summary>
    /// Reconstructs the list of sprites from the saved card data in PlayerPrefs.
    /// </summary>
    /// <returns>A list of Sprites corresponding to the saved board layout.</returns>
    public List<Sprite> GetSuffuleList()
    {
        List<Sprite> suffledSpriteList = new List<Sprite>();
        List<string> cardData = PlayerPrefsHalper.SAVED_GAME_CARD_DATA;
        for (int i = 0; i < cardData.Count; i++)
        {
            suffledSpriteList.Add(gameController.gameInfo.allCardSprites[int.Parse(cardData[i]) - 1]);
        }
        return suffledSpriteList;
    }
    
}
