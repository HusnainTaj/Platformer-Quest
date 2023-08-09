using UnityEngine;

public class OneWayCollisionPlatform : MonoBehaviour {

    private Collider2D playerCollider;

    [SerializeField]
    private Collider2D platformCollider;
    [SerializeField]
    private Collider2D platformTrigger;

    //[SerializeField]
    //private float BounceForce;

    void Start ()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
    }

    //void OnCollisionEnter2D(Collision2D collider)
    //{
    //    if (gameObject.tag == "BouncyBox" && collider.gameObject.tag == "Player")
    //        collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, BounceForce));
    //}

}
