using UnityEngine;
using System.Collections;

public class FollowNodes : MonoBehaviour {
    public Transform[] nodesToFollow;
    public float delayAtNodes = 2f;
    public float movementSpeed = 1f;
    public bool mirrorPath = false;

    bool goingUp = true;
    bool pauseMove = false;
    int currentNode = 0;
    int nextNode = 1;

	// Use this for initialization
	void Start () {
        transform.position = nodesToFollow[0].position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!pauseMove)
        {
            Vector3 moveDirection = (nodesToFollow[nextNode].position - transform.position).normalized;
                    //Sets the direction we want to go
            transform.position = transform.position + ((moveDirection * movementSpeed) * Time.deltaTime);
                    //Sets the new position of the object ignoring collisions (can transport through colliders)
            if ((transform.position - nodesToFollow[nextNode].position).magnitude < .02) //Checks if we are within a margin of error
            {
                if (!pauseMove)
                {
                    pauseMove = true; //Set this to not move
                    Invoke("UnpauseMove", delayAtNodes); //sets us to move after delay time
                    transform.position = nodesToFollow[nextNode].position; //forces us to exact coordinates
                }

                if (mirrorPath)
                {
                    if (goingUp)
                    {
                        nextNode++;
                        currentNode++;
                                //Go to the next node

                        if (nextNode >= nodesToFollow.Length)
                        {
                            //If we are at the end of the nodes, start going down
                            goingUp = false;
                            nextNode = nodesToFollow.Length - 2;
                            currentNode = nodesToFollow.Length - 1;
                        }
                    }
                    else
                    {
                        nextNode--;
                        currentNode--;
                                //Go to previous node

                        if (nextNode < 0)
                        {
                            //if we get back to the start, flip around
                            goingUp = true;
                            nextNode = 1;
                            currentNode = 0;
                        }
                    }
                }
                else
                {
                    //Loops through nodes if mirror is off.
                    nextNode++;
                    currentNode++;
                    if (nextNode >= nodesToFollow.Length)
                    {
                        nextNode = 0;
                    }
                    if (currentNode >= nodesToFollow.Length)
                    {
                        currentNode = 0;
                    }
                }
            }
        }
	}

    void UnpauseMove()
    {
        pauseMove = false;
    }
}
