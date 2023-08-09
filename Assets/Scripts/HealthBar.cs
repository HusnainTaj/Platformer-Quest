using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private Image Bar;

    [SerializeField]
    private float lerpSpeed;

    private static float currentHealth;

    private float maxHealth = 50f;

    public Player player;

    private GameManager GM;

	void Start ()
    {
        GM = FindObjectOfType<GameManager>();

        currentHealth = maxHealth;
	}
	
	void Update ()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GM.Die();
        }
		else if (currentHealth >= maxHealth)
        {
			currentHealth = maxHealth;
        }

        UpdateHealthBar();
	}

    void UpdateHealthBar()
    {
        Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, currentHealth / maxHealth, Time.deltaTime * lerpSpeed);
    }

    public static void decreaseHealth(float amount)
    {
        currentHealth -= amount;
    }

    public static void increaseHealth(float amount)
    {
        currentHealth += amount;
    }
}
