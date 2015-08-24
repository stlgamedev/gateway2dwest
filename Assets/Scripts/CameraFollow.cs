using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {
    public List<Transform> objectsToFollow;
    public BoxCollider2D boundingBox;

    bool isShaking = false;
    float shakeStrength = .5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 centerPos = Vector3.zero;
        float distance = 0;
                //Setting up variables for later use.


        for(int i = 0; i < objectsToFollow.Count; i++)
        {
            centerPos += objectsToFollow[i].position; //Add to center position for averaging later
            if(i != objectsToFollow.Count-1)
            {
                distance += Vector3.Distance(objectsToFollow[i].position, objectsToFollow[i + 1].position);
                        //If we aren't the last object, add the distance of this object to the next
            }
        }

        centerPos = (centerPos / objectsToFollow.Count);
                //Average the positions of the objects to find the center

        distance = (distance / objectsToFollow.Count);
                //Averages the distance of all the objects

        transform.position = new Vector3(
                                    Mathf.Clamp(centerPos.x, boundingBox.bounds.min.x, boundingBox.bounds.max.x),
                                    Mathf.Clamp(centerPos.y, boundingBox.bounds.min.y, boundingBox.bounds.max.y),
                                    - 10);
                //Sets the position to center of all objects, then clamps it to fit inside the camera bounds object


        if (distance > 5)
        {
            GetComponent<Camera>().orthographicSize = 6 + (distance - 5);
                    //Only changes the orthographic view if the distance average is greater then 5 units apart.
        }
        CheckShake();
        //Clamps the camera transform position to the bounds of the CameraBounds object in the scene.
	}

    void CheckShake()
    {
        if(isShaking)
        {
            transform.position += new Vector3(Random.Range(-shakeStrength,shakeStrength),Random.Range(-shakeStrength,shakeStrength),0);
        }
    }

    public void ShakeCamera(float duration = .5f, float strength = .5f, bool forceShake = false)  // Shake the camera, with optional values. ShakeCamera() will simply use .5 in place of there being no values this way.
    {
        
        if(!IsInvoking("StopCameraShake") || forceShake)
        {
            //Only apply this value if there is no shaking already or it is forced with the flag.
            shakeStrength = strength;
            Invoke("StopCameraShake", duration);
            isShaking = true;
        }
    }

    public void StopCameraShake()
    {
        isShaking = false;
    }
}
