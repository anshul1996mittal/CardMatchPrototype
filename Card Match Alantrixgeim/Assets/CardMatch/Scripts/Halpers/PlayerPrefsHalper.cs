
using UnityEngine;
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
}
