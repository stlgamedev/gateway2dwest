using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 2.0f;

	public InputHelper inputHelper;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var axis = inputHelper.axisRaw ();
		if (axis != Vector2.zero) {
			transform.position += new Vector3(axis.x * movementSpeed * Time.deltaTime, axis.y * movementSpeed * Time.deltaTime, 0);
		}
	}
}
