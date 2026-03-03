
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
}
