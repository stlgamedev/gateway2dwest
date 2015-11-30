using UnityEngine;
using System.Collections;
using System;

public class InputHelper : MonoBehaviour {
	public string horizontalAxis = "Horizontal1";
    public string rightButton = "d";
    public string leftButton = "a";
    public string upButton = "w";
    public string downButton = "s";
	public string verticalAxis = "Vertical1";
    public string attackButton = "Player 1 Attack";
    public string magicButton = "Player 1 Magic";
    public string confirm = "Player 1 Confirm";
    public string cancel = "Player 1 Cancel";
    public string select = "Player 1 Select";

    public virtual Vector2 axisRaw () {
        var x = Input.GetAxisRaw(horizontalAxis);

        //Adds support for using keys as axis if no axis is used.
        //Similar to how unity works with axis, pressing both values equals neither value.
        //This is to allow for both keyboards/buttons and joystick/trigger support at the same time.
        if (x == 0)
        {
            try
            {
                x = Convert.ToInt32(Input.GetKey(rightButton)) - Convert.ToInt32(Input.GetKey(leftButton));
            }catch(Exception ex)
            {
                //Only sets the value if it can find a valid value for both left and right buttons.
            }
        }
        var y = Input.GetAxisRaw (verticalAxis);
        if (y == 0)
        {
            try
            {
                y = Convert.ToInt32(Input.GetKey(upButton)) - Convert.ToInt32(Input.GetKey(downButton));
            }
            catch (Exception ex)
            {
                //Only sets the value if it can find a valid value for both up and down buttons.
            }
        }


        return new Vector2(x, y); ;
	}

    public virtual bool GetRightDown()
    {
        if (rightButton != "")
        {
            return Input.GetKeyDown(rightButton);
        }
        else
        {
            return false;
        }
    }

    public virtual bool GetLeftDown()
    {
        if(leftButton != "")
        { 
            return Input.GetKeyDown(leftButton);
        }
        else
        {
            return false;
        }
    }

    public virtual bool GetUpDown()
    {
        if(upButton != "")
        { 
            return Input.GetKeyDown(upButton);
        }
        else
        {
            return false;
        }
    }

    public virtual bool GetDownDown()
    {
        if (downButton != "")
        {
            return Input.GetKeyDown(downButton);
        }
        else
        {
            return false;
        }
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
    public virtual bool GetMagicDown()
    {
        return Input.GetButtonDown(magicButton);
    }
    public virtual bool GetMagicUp()
    {
        return Input.GetButtonUp(magicButton);
    }
    public virtual bool GetMagic()
    {
        return Input.GetButton(magicButton);
    }
}
