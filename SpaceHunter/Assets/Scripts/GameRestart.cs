using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    public static GameRestart S { get; private set; }

    private void Awake()
    {
        S = this;
    }

    public void DelayedRestart(float delay)
    {
        Invoke(nameof(Restart), delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}