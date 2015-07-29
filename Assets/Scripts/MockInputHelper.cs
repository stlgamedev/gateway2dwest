using UnityEngine;
using System.Collections;

public class MockInputHelper : InputHelper {
	
	public bool allowMockInput = true;
	
	public Vector2 mockAxisRaw = new Vector2(0, 0);

		// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override Vector2 axisRaw () {
		if (allowMockInput) {
			return mockAxisRaw;
		} else {
			return base.axisRaw();
		}
	}
}
