
using UnityEngine;
using System.Collections.Generic;
public class PlayerPrefsHalper 
{
    public static int ROW_COUNT
        {
            get { return PlayerPrefs.GetInt("RowCount"); }
            set { PlayerPrefs.SetInt("RowCount", value); }
        }
        public static int COLUMN_COUNT
        {
            get { return PlayerPrefs.GetInt("ColumnCount"); }
            set { PlayerPrefs.SetInt("ColumnCount", value); }
        }
        public static int HIGH_SCORE
        {
            get { return PlayerPrefs.GetInt("HighScore"); }
            set { PlayerPrefs.SetInt("HighScore", value); }
        }
    public static bool IS_SOUND_ON
    {
        get { return PlayerPrefs.GetInt("IsSoundOn")==1; }
        set { PlayerPrefs.SetInt("IsSoundOn", value?1:0); }
    }

    public static float SOUNDFX_VOLUME
    {
        get { return PlayerPrefs.GetFloat("SoundFXValue"); }
        set { PlayerPrefs.SetFloat("SoundFXValue", value ); }
    }
    public static float MUSUSIC_VOLUME
    {
        get { return PlayerPrefs.GetFloat("MusicValue"); }
        set { PlayerPrefs.SetFloat("MusicValue", value); }
    }

    #region GameSave
    public static bool IS_GAME_SAVED
    {
        get { return PlayerPrefs.GetInt("IsGameSaved") == 1; }
        set { PlayerPrefs.SetInt("IsGameSaved", value ? 1 : 0); }
    }
    public static int COUNT_CORRECT_CARD_FLIP
    {
        get { return PlayerPrefs.GetInt("CountCorrectCardFlip"); }
        set { PlayerPrefs.SetInt("CountCorrectCardFlip", value); }
    }
    public static float SAVED_GAME_TIMER
    {
        get { return PlayerPrefs.GetFloat("SavedGameTimer"); }
        set { PlayerPrefs.SetFloat("SavedGameTimer", value); }
    }
    public static int SAVED_GAME_CURRENT_SCORE
    {
        get { return PlayerPrefs.GetInt("SavedGameScore"); }
        set { PlayerPrefs.SetInt("SavedGameScore", value); }
    }
    public static List<string> SAVED_GAME_CARD_DATA
    {
        get {
            List<string> cardData = new List<string>();
            for(int i=0;i< COLUMN_COUNT * ROW_COUNT; i++)
            {
                cardData.Add(PlayerPrefs.GetString("SavedGameCardData" + i));
            }
            return cardData; }
        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                PlayerPrefs.SetString("SavedGameCardData" + i, value[i]);
            }
        }
    }
    public static List<bool> SAVED_GAME_CARD_FLIPPED_DATA
    {
        get
        {
            List<bool> cardFlipedData = new List<bool>();
            for (int i = 0; i < COLUMN_COUNT * ROW_COUNT; i++)
            {
                cardFlipedData.Add(PlayerPrefs.GetInt("SavedGameCardFlipedData" + i)==1);
            }
            return cardFlipedData;
        }
        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                PlayerPrefs.SetInt("SavedGameCardFlipedData" + i, value[i]?1:0);
            }
        }
    }
    #endregion
}
