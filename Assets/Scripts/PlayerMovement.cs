using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 2.0f;
	public InputHelper inputHelper;
    public int playerID = 0; //Setting foundation for multiple players
    
    Animator animator;
    Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> (); //In unity, GetComponent is implicit to the object the script is attached, thus "this" is not needed
        rb = GetComponent<Rigidbody2D>(); //Gets attached rigidbody2D component
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 axis = inputHelper.axisRaw(); //Get's input from helper class. This allows for unit testing as well as changing input while inside of the game.
                            //Perhaps we can also use this as a way to define different controls for different players by feeding it the playerID.

		if ( Mathf.Abs(axis.magnitude) >= 1) { //Checks that values are not greater then one for input. Checks absolute value to fix errors in positive and negative directions.
			axis.Normalize ();
		}
		if (Mathf.Abs (axis.x) > Mathf.Abs (axis.y)) {
			updateAnimationParameters(axis.x, 0.0f);//Sets up walking left/right animation
		} else if(Mathf.Abs (axis.x) < Mathf.Abs (axis.y)) {
			updateAnimationParameters(0.0f, axis.y);//Sets up walking up/down animation
		} else if(axis == Vector2.zero) {//Checking if it's equal to zero will allow animations when sliding against walls to still work
            updateAnimationParameters(0, 0);//sets up idle animation
		}


		//transform.position += new Vector3 (axis.x * movementSpeed * inputHelper.deltaTime, axis.y * movementSpeed * inputHelper.deltaTime, 0);
        //Setting a transform.position instead of using a system like a rigidbody ignores colliders.


        rb.velocity = axis * movementSpeed; //Unity automatically applies a delta time to rigidbody velocity.
                            //Even if it's a vector3, unity will accept a vector2 and automatically add 0 as the third vector.
                            //This removes the need to implicitly make a vector3.
	}

	void updateAnimationParameters (float x, float y)
	{
		if (animator) {
			animator.SetFloat ("HorizontalMovement", x);
			animator.SetFloat ("VerticalMovement", y);
		}
	}
}
