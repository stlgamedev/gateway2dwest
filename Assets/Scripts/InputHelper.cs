using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {
	public string horizontalAxis = "Horizontal1";
	public string verticalAxis = "Vertical1";
	
	public virtual Vector2 axisRaw () {
		var x = Input.GetAxisRaw (horizontalAxis);
		var y = Input.GetAxisRaw (verticalAxis);
		return new Vector2(x, y);
	}
}
