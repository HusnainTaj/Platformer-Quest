using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject otherPortal;

    private bool transfer;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !transfer && otherPortal != null)
        {
            otherPortal.GetComponent<Portal>().transfer = true;
            collider.transform.position = otherPortal.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            transfer = false;
        }
    }
}