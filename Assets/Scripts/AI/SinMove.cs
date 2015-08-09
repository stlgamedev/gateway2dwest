using UnityEngine;
using System.Collections;

public class SinMove : MonoBehaviour {
    public float movementSpeedMultiplyer = 1;
    public Vector2 moveRange = new Vector2(1,1);
    public float startOffset = 0;

    Vector3 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = startPos + new Vector3(Mathf.Sin((Time.realtimeSinceStartup * movementSpeedMultiplyer)+startOffset) * moveRange.x, Mathf.Sin((Time.realtimeSinceStartup * movementSpeedMultiplyer)+startOffset) * moveRange.y, 0);
	}
}
