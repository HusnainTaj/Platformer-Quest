using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour {

    [SerializeField]
    GameObject backDrop;

    [SerializeField]
    GameObject loadingScreen;

    public SceneFader sceneFader;

    Animator LikePanelAnimtor;
    Animator creditsPanelAnimtor;
    Animator levelSelectPanelAnimtor;
    
    private void Start()
    {
        LikePanelAnimtor = GameObject.Find("LikePanel").GetComponent<Animator>();
        creditsPanelAnimtor = GameObject.Find("CreditsPanel").GetComponent<Animator>();
        levelSelectPanelAnimtor = GameObject.Find("LevelSelectPanel").GetComponent<Animator>();
        
    }

    public void PlayBtn()
    {
        backDrop.SetActive(true);
        levelSelectPanelAnimtor.SetBool("LevelSelect", true);
    }

    public void CreditsBtn()
    {
        backDrop.SetActive(true);
        creditsPanelAnimtor.SetBool("Settings", true);
    }

    public void ResetBtn(GameObject ResetPanel)
    {
        ResetPanel.SetActive(true);
        backDrop.SetActive(true);
    }

    public void ResetYesBtn()
    {
        PlayerPrefs.DeleteAll();
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetNoBtn(GameObject ResetPanel)
    {
        ResetPanel.SetActive(false);
        backDrop.SetActive(false);
    }

    public void LikeBtn()
    {
        backDrop.SetActive(true);
        LikePanelAnimtor.SetBool("Like", true);
    }

    public void DonateBtn(string url)
    {
        Application.OpenURL("http://" + url);
    }

    public void BackToMenu()
    {
        LikePanelAnimtor.SetBool("Like", false);
        creditsPanelAnimtor.SetBool("Settings", false);
        levelSelectPanelAnimtor.SetBool("LevelSelect", false);
        backDrop.SetActive(false);
    }

    public void LoadLevelBtn(int sceneID)
    {
        sceneFader.FadeTo(sceneID);
    }
}
