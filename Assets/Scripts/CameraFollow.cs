using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float xMin;

    [SerializeField]
    private float yMin;

    public float xMax;

    [SerializeField]
    private float yMax;

    private Transform target;

    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void LateUpdate ()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
	}
}
