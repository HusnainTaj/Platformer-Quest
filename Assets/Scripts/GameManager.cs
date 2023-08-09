using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private GameObject player;

    public GameObject BackDrop;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject[] Stars;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Win(Animator playerAnimator, Rigidbody2D playerRigibody, int gems)
    {
        playerAnimator.enabled = false;
        playerRigibody.velocity = new Vector2(0, 0);
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_happy");

        BackDrop.SetActive(true);
        WinPanel.SetActive(true);
        Stars[gems].SetActive(true);

        PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex + "Stars", gems);

        if (PlayerPrefs.GetInt("currentLevel") <= SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex != 10)
            PlayerPrefs.SetInt("currentLevel", SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Die()
    {
        player.gameObject.SetActive(false);
        BackDrop.SetActive(true);
        GameOverPanel.SetActive(true);
    }
}
