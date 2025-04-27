using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private static float _oldTimeScale = 1f;

    public void PauseGame()
    {
        _oldTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = _oldTimeScale;
    }

    public void QuitGame()
    {
        GameManager.GMInstance.QuitGame();
    }
}
