using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 2.0f;
	public InputHelper inputHelper;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		var axis = inputHelper.axisRaw();
		if (axis != Vector2.zero) {
			if (axis.magnitude >= 1) {
				axis.Normalize ();
			}
			if (Mathf.Abs (axis.x) > Mathf.Abs (axis.y)) {
				updateAnimationParameters(axis.x, 0.0f);
			} else {
				updateAnimationParameters(0.0f, axis.y);
			}
			transform.position += new Vector3 (axis.x * movementSpeed * inputHelper.deltaTime, axis.y * movementSpeed * inputHelper.deltaTime, 0);
		} else {
			updateAnimationParameters(0, 0);
		}
	}

	void updateAnimationParameters (float x, float y)
	{
		if (animator) {
			animator.SetFloat ("HorizontalMovement", x);
			animator.SetFloat ("VerticalMovement", y);
		}
	}
}
