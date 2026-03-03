using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloder : MonoBehaviour
{
    [Range(1,5)]
    public int splashTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("MainMenuLoad", splashTimer);
    }

   private void MainMenuLoad()
    {
        GameManager.Instance.SceneLoad("MainMenu");
    }
}
