using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public float hitPoints = 100;

	public bool poisioned = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (poisioned) {
			hitPoints -= Time.deltaTime * 10.0f;
		}
	}
}
