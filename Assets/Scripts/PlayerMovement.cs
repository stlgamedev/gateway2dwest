using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

	public float movementSpeed = 2.0f;
    public float damagedMovementSpeed = 4f;
	public InputHelper inputHelper;
	
    
	Animator animator;
	Rigidbody2D rb;
    PlayerStatus playerStats;
    Vector2 axis;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> (); //In unity, GetComponent is implicit to the object the script is attached, thus "this" is not needed
		rb = GetComponent<Rigidbody2D> (); //Gets attached rigidbody2D component
        playerStats = GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		axis = inputHelper.axisRaw ();			//Get's input from helper class. This allows for unit testing as well as changing input while inside of the game.
		axis = EnsurePlayerNeverMovesFasterThanMaxSpeed (axis);
		UpdateAnimationStates (axis);
	}

    void FixedUpdate()
    {
        ApplyMotion(axis);//Moved into Fixed Update since it's a physics update. This should help fix bugs with getting caught between colliders and such.
    }

    private void ApplyMotion(Vector2 moveDirection)
    {
        if (!playerStats.takingDamage)
        {
            rb.velocity = moveDirection * movementSpeed; //Normal Movement
        }
        else
        {
            rb.velocity = (transform.position - playerStats.attackerPos).normalized * damagedMovementSpeed;
            //Calculates direction from the object attacking us and pushes us away at a set speed while taking damage.
            //This may be changed later to slower speed when damaged. It hasn't been decided yet.
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 1000);
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
