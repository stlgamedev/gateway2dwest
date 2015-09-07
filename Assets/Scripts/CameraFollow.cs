using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform[] objectsToFollow = new Transform[4];
    public BoxCollider2D boundingBox;

    bool isShaking = false;
    float shakeStrength = .5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 centerPos = Vector3.zero;
        float distance = 0;
        //Setting up variables for later use.

        Vector2 minPos = objectsToFollow[0].position;
        Vector2 maxPos = objectsToFollow[0].position;
        //If we set it to 0 and the level starts at 0, then the minimum position is always 0.
        //Setting it to the first object to follow means that there will never be this minimum issue.

        //Checks all objects to follow for the maximum and minimum values to calculate camera position and distance.
        for (int i = 0; i < objectsToFollow.Length; i++)
        {
            if (objectsToFollow[i].position.x < minPos.x)
            {
                minPos.x = objectsToFollow[i].position.x;
            }
            if (objectsToFollow[i].position.y < minPos.y)
            {
                minPos.y = objectsToFollow[i].position.y;
            }
            if (objectsToFollow[i].position.x > maxPos.x)
            {
                maxPos.x = objectsToFollow[i].position.x;
            }
            if (objectsToFollow[i].position.y > maxPos.y)
            {
                maxPos.y = objectsToFollow[i].position.y;
            }
        }
        distance = Vector2.Distance(minPos, maxPos);
        //calculate the distance between the minimum and maximum positions to calculate zoom.

        centerPos = (minPos + maxPos) / 2;
        //Average the positions of the min/max to find the center of all players

        transform.position = new Vector3(
                                    Mathf.Clamp(centerPos.x, boundingBox.bounds.min.x, boundingBox.bounds.max.x),
                                    Mathf.Clamp(centerPos.y, boundingBox.bounds.min.y, boundingBox.bounds.max.y),
                                    -10);
        //Sets the position to center of all objects, then clamps it to fit inside the camera bounds object

        GetComponent<Camera>().orthographicSize = Mathf.Max(5f, (distance * .5f) + 1);
        //Changes the orthographic view to the minimum of 5, but will zoom to fit all players
        //Since we are centered, we use half the distance, and add 1 to allow for the height/width of player images

        CheckShake(); //Check to see if we should apply the camera shake.
    }

    void CheckShake()
    {
        if (isShaking)
        {
            transform.position += new Vector3(Random.Range(-shakeStrength, shakeStrength), Random.Range(-shakeStrength, shakeStrength), 0);
            //Offsets the camera by a random value with the force of shakeStrength
        }
    }

    public void ShakeCamera(float duration = .5f, float strength = .5f, bool forceShake = false)  // Shake the camera, with optional values. ShakeCamera() will simply use .5 in place of there being no values this way.
    {

        if (!IsInvoking("StopCameraShake") || forceShake)
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
