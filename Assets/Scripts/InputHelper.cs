using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual Vector2 axisRaw () {
		var x = Input.GetAxisRaw (horizontalAxis);
		var y = Input.GetAxisRaw (verticalAxis);
		return new Vector2(x, y);
	}
}
