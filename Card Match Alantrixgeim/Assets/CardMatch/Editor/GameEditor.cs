using UnityEditor;
using UnityEngine;

public class GameEditor : EditorWindow
{

    int rowCount;
    int coulmnCount;
    [MenuItem("AlantrixGeim/GameEditor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GameEditor window = (GameEditor)EditorWindow.GetWindow(typeof(GameEditor));
        window.Show();
    }
    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        GUILayout.Label(" Row & Column ");
        rowCount = EditorGUILayout.IntField("", rowCount);
        if (GUILayout.Button("Set Row"))
        {
            PlayerPrefsHalper.ROW_COUNT=rowCount;
        }
        coulmnCount = EditorGUILayout.IntField("", coulmnCount);
        if (GUILayout.Button("Set Column"))
        {
            PlayerPrefsHalper.COLUMN_COUNT = coulmnCount;
        }
    }
}
