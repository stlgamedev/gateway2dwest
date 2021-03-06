﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

	public float movementSpeed = 2.0f;
    public float damagedMovementSpeed = 4f;
	public InputHelper inputHelper;
	public float knockbackStunTime = 0.08f;
    public GameObject weapon;
    public AudioClip attackSound;

    private bool disableControls;
	private Vector2 knockbackDirection;
    
	Animator animator;
	Rigidbody2D rb;
    Status playerStats;
    Vector2 axis;
	
	void Start ()
	{
		animator = GetComponent<Animator> (); //In unity, GetComponent is implicit to the object the script is attached, thus "this" is not needed
		rb = GetComponent<Rigidbody2D> (); //Gets attached rigidbody2D component\
	}
	
	// Update is called once per frame
	void Update ()
	{
		axis = inputHelper.axisRaw ();			//Get's input from helper class. This allows for unit testing as well as changing input while inside of the game.
		axis = EnsurePlayerNeverMovesFasterThanMaxSpeed (axis);
		UpdateAnimationStates(axis);
        CheckAttack();
	}

    void FixedUpdate()
    {
        ApplyMotion(axis);//Moved into Fixed Update since it's a physics update. This should help fix bugs with getting caught between colliders and such.
    }

    void CheckAttack()
    {
        if (inputHelper.GetAttackDown())
        {
            weapon.GetComponent<Animator>().SetBool("isAttacking", true);
            SoundManager.instance.PlaySingle(attackSound);
        }
    }

    private void ApplyMotion(Vector2 moveDirection)
    {
        if (!disableControls)
        {
            rb.velocity = moveDirection * movementSpeed; //Normal Movement
        }
        else
        {
            rb.velocity = knockbackDirection * damagedMovementSpeed;
            //Calculates direction from the object attacking us and pushes us away at a set speed while taking damage.
            //This may be changed later to slower speed when damaged. It hasn't been decided yet.
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 1000);
    }

	public virtual void KnockBack(Vector2 direction) {
		disableControls = true;
		knockbackDirection = direction;
		Invoke("ResumeControl", knockbackStunTime); //resets taking damage flag
	}

    public void FreezeControl()
    {
        disableControls = true;
    }

	public void ResumeControl() {
        weapon.GetComponent<Animator>().SetBool("isAttacking", false);
		disableControls = false;
		knockbackDirection = Vector2.zero;
	}

	private void UpdateAnimationStates (Vector2 axis)
	{
        Vector2 boxOffset = weapon.GetComponent<BoxCollider2D>().offset;
        if (Mathf.Abs (axis.x) > Mathf.Abs (axis.y)) {
			SetAnimationParameters (axis.x, 0.0f);
            if(axis.x < 0)
            {
                weapon.transform.eulerAngles = new Vector3(0, 0, -90);
                boxOffset = new Vector2(0, boxOffset.y);
                weapon.transform.localPosition = new Vector3(.15f, .35f, 0);
            }
            else
            {
                weapon.transform.eulerAngles = new Vector3(0, 0, 90);
                boxOffset = new Vector2(0,boxOffset.y);
                weapon.transform.localPosition = new Vector3(-.15f, .35f, 0);
            }
			//Sets up walking left/right animation
		} else if (Mathf.Abs (axis.x) < Mathf.Abs (axis.y)) {
			SetAnimationParameters (0.0f, axis.y);
            if(axis.y < 0)
            {
                weapon.transform.eulerAngles = new Vector3(0, 0, 0);
                boxOffset = new Vector2(0, boxOffset.y);
                weapon.transform.localPosition = new Vector3(-.15f, .57f, 0);
            }
            else
            {
                weapon.transform.eulerAngles = new Vector3(0, 0, 180);
                boxOffset = new Vector2(0, boxOffset.y);
                weapon.transform.localPosition = new Vector3(.15f, .2f, 0);
            }
			//Sets up walking up/down animation
		} else if (axis == Vector2.zero) {
			//Checking if it's equal to zero will allow animations when sliding against walls to still work
			SetAnimationParameters (0, 0);
			//sets up idle animation
		}
	}

	private void SetAnimationParameters (float x, float y)
	{
		if (animator) {
			animator.SetFloat ("HorizontalMovement", x);
			animator.SetFloat ("VerticalMovement", y);
		}
	}

	private Vector2 EnsurePlayerNeverMovesFasterThanMaxSpeed (Vector2 axis)
	{
		if (Mathf.Abs (axis.magnitude) >= 1) {
			axis.Normalize ();
		}
		return axis;
	}
}
