
using UnityEngine;
public class PlayerPrefsHalper 
{
    public static string PLAYER_NAME
        {
            get { return PlayerPrefs.GetString("PLAYER_NAME"); }
            set { PlayerPrefs.SetString("PLAYER_NAME", value); }
        }
}
