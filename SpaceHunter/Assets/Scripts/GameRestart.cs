using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    private static GameRestart s;

    public static GameRestart S => s;
    
    public void DelayedRestart(float delay)
    {
        Invoke(nameof(Restart), delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void Awake()
    {
        s = this;
    }
}
