using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {
	public string horizontalAxis = "Horizontal1";
	public string verticalAxis = "Vertical1";
    public string attackButton = "Player 1 Attack";
	
	public virtual Vector2 axisRaw () {
		var x = Input.GetAxisRaw (horizontalAxis);
		var y = Input.GetAxisRaw (verticalAxis);
		return new Vector2(x, y);
	}

    public virtual bool GetAttackDown()
    {
        return Input.GetButtonDown(attackButton);
    }
    public virtual bool GetAttackUp()
    {
        return Input.GetButtonUp(attackButton);
    }
    public virtual bool GetAttack()
    {
        return Input.GetButton(attackButton);
    }
}
