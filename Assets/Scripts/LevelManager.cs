using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private int currentLevel;

    private int starsCollected = 0;

    public Text starCountText;

    public Button[] levelBtns;

	void Start ()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel", 1);

        for (int i = 1; i < 11; i++)
        {
            starsCollected += PlayerPrefs.GetInt("Level" + i + "Stars", 0);
        }

        starCountText.text = starCountText.text + starsCollected;

        for (int i = 1; i <= currentLevel; i++)
        {
            try
            {
                levelBtns[i].interactable = true;
                levelBtns[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            catch
            {
                return;
            }
        }
	}
}
