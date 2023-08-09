using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    [SerializeField]
    [Range(min:2, max:10)]
    [Tooltip("Higher the speed value slower the backgeound moves.")]
    private float speed;

	void LateUpdate ()
    {
        transform.position = new Vector2(Input.mousePosition.x / speed, Input.mousePosition.y / speed);
	}
}
