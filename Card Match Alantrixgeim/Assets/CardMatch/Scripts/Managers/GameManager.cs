using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

/// <summary>
/// Manages global game state, scene transitions, and utility coroutines.
/// Persists across scene loads.
/// </summary>
public sealed class GameManager : Singleton<GameManager>
{
    void Awake()
    {
        if (IsAwake)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    /// <summary>
    /// Loads a scene by its name.
    /// </summary>
    /// <param name="sceneName">The exact name of the scene to load.</param>
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Coroutine that waits for a specified time before executing a callback.
    /// </summary>
    /// <param name="time">Time to wait in seconds.</param>
    /// <param name="isRealTime">If true, uses unscaled time (ignores time scale).</param>
    /// <param name="callback">The action to execute after the delay.</param>
    public IEnumerator ActionCallAfterTime(float time, bool isRealTime, Action callback)
    {
        // Wait for the specified duration, checking if we should use real-time or game-time
        yield return isRealTime ? new WaitForSecondsRealtime(time) : new WaitForSeconds(time);
        
        // Execute the callback if it's not null
        callback?.Invoke();
    }
}
