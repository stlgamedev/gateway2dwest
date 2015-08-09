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
            transform.position = transform.position + ((moveDirection * movementSpeed) * Time.deltaTime);
            if ((transform.position - nodesToFollow[nextNode].position).magnitude < .02)
            {
                pauseMove = true;
                Invoke("UnpauseMove", delayAtNodes);
                transform.position = nodesToFollow[nextNode].position;

                if (mirrorPath)
                {
                    if (goingUp)
                    {
                        nextNode++;
                        currentNode++;
                        if (nextNode >= nodesToFollow.Length)
                        {
                            goingUp = false;
                            nextNode = nodesToFollow.Length - 2;
                            currentNode = nodesToFollow.Length - 1;
                        }
                    }
                    else
                    {
                        nextNode--;
                        currentNode--;
                        if (nextNode < 0)
                        {
                            goingUp = true;
                            nextNode = 1;
                            currentNode = 0;
                        }
                    }
                }
                else
                {
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
