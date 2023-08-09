using UnityEngine;

public class Rotator : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed;

    void FixedUpdate ()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
