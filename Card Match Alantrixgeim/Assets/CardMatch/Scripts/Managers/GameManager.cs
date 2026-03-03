using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
