using UnityEngine;
using System.Collections;

public class ZSort : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 1000);
	}
}
