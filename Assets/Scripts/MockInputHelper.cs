using UnityEngine;
using System.Collections;

public class MockInputHelper : InputHelper {
	
	public bool allowMockInput = true;
	
	public Vector2 mockAxisRaw = new Vector2(0, 0);

	public override Vector2 axisRaw () {
		if (allowMockInput) {
			return mockAxisRaw;
		} else {
			return base.axisRaw();
		}
	}
}
