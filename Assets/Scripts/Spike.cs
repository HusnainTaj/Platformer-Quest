using UnityEngine;

public class Spike : MonoBehaviour {

    private float damage = 15;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.Hurt(damage);
        }
    }
}
