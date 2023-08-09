using UnityEngine;

public class Saw : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;

    public Transform[] points;

    private int currentPoint;

    private float damage = 20;

    private Player player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        transform.position = points[0].position;
        currentPoint = 0;
	}
	
	void FixedUpdate ()
    {
        if (transform.position == points[currentPoint].position)
        {
            currentPoint++;
        }

        if (currentPoint >= points.Length)
        {
            currentPoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].localPosition, moveSpeed * Time.deltaTime);
	}

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.Hurt(damage);
        }
    }
}
