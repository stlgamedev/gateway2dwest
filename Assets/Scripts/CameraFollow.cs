using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject objectToFollow;
    public BoxCollider2D boundingBox;

    bool isShaking = false;
    float shakeStrength = .5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = new Vector3(
                                    Mathf.Clamp(objectToFollow.transform.position.x, boundingBox.bounds.min.x, boundingBox.bounds.max.x),
                                    Mathf.Clamp(objectToFollow.transform.position.y, boundingBox.bounds.min.y, boundingBox.bounds.max.y),
                                    - 10);
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

    public void ShakeCamera(float duration = .5f, float strength = .5f)
    {
        shakeStrength = strength;
        Invoke("StopCameraShake", duration);
        isShaking = true;
    }

    public void StopCameraShake()
    {
        isShaking = false;
    }
}
