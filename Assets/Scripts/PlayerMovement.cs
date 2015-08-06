using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

	public float movementSpeed = 2.0f;
	public InputHelper inputHelper;
	public int playerID = 0; //Setting foundation for multiple players
    
	Animator animator;
	Rigidbody2D rb;


	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> (); //In unity, GetComponent is implicit to the object the script is attached, thus "this" is not needed
		rb = GetComponent<Rigidbody2D> (); //Gets attached rigidbody2D component
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 axis = inputHelper.axisRaw ();			//Get's input from helper class. This allows for unit testing as well as changing input while inside of the game.

		axis = EnsurePlayerNeverMovesFasterThanMaxSpeed (axis);
		UpdateAnimationStates (axis);

		rb.velocity = axis * movementSpeed;
	}

	private void UpdateAnimationStates (Vector2 axis)
	{
		if (Mathf.Abs (axis.x) > Mathf.Abs (axis.y)) {
			SetAnimationParameters (axis.x, 0.0f);
			//Sets up walking left/right animation
		} else if (Mathf.Abs (axis.x) < Mathf.Abs (axis.y)) {
			SetAnimationParameters (0.0f, axis.y);
			//Sets up walking up/down animation
		} else if (axis == Vector2.zero) {
			//Checking if it's equal to zero will allow animations when sliding against walls to still work
			SetAnimationParameters (0, 0);
			//sets up idle animation
		}
	}

	private void SetAnimationParameters (float x, float y)
	{
		animator.SetFloat ("HorizontalMovement", x);
		animator.SetFloat ("VerticalMovement", y);
	}

	private Vector2 EnsurePlayerNeverMovesFasterThanMaxSpeed (Vector2 axis)
	{
		if (Mathf.Abs (axis.magnitude) >= 1) {
			axis.Normalize ();
		}
		return axis;
	}
}
