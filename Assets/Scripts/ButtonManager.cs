using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    [SerializeField]
    GameObject backDrop;

    public SceneFader sceneFader;

    public void PlayBtn(GameObject LevelStartPanel)
    {
        FindObjectOfType<Player>().enabled = true;
        backDrop.SetActive(false);
        LevelStartPanel.SetActive(false);
    }

    public void PauseBtn(GameObject PausedPanel)
    {
        backDrop.SetActive(true);
        PausedPanel.SetActive(true);
    }

    public void ResumeBtn(GameObject PausedPanel)
    {
        backDrop.SetActive(false);
        PausedPanel.SetActive(false);
    }

    public void RestartBtn()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitBtn()
    {
        sceneFader.FadeTo(0);
    }

    public void NextLevelBtn()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }
}