using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 2.0f;
	public InputHelper inputHelper;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var axis = inputHelper.axisRaw();
		if (axis != Vector2.zero) {
			if (axis.magnitude >= 1) {
				axis.Normalize();
			}
			transform.position += new Vector3 (axis.x * movementSpeed * inputHelper.deltaTime, axis.y * movementSpeed * inputHelper.deltaTime, 0);
		}
	}
}
