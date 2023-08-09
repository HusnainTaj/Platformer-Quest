using UnityEngine;

public class DoorKey : MonoBehaviour {

    private Sprite DoorOpened;

	void Start ()
    {
        DoorOpened = Resources.Load<Sprite>("DoorOpened");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = DoorOpened;
            gameObject.transform.parent.gameObject.tag = "DoorOpened";
            gameObject.SetActive(false);
        }
    }
}
