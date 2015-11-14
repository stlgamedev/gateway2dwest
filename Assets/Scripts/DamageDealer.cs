using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {

    public float damageToDeal = 1;
    public LayerMask objectsToDealDamage;

    void OnCollisionStay2D(Collision2D col)
    {
		handleCollision(col.collider);
    }

    void OnTriggerStay2D(Collider2D col)
    {
		handleCollision(col);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		handleCollision(col.collider);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
		handleCollision (col);
    }

	private void handleCollision (Collider2D col)
	{
        //I copied the layermask check from an answers page on the web
        //it's a comparison to check if the collider layermask is inside the bitwise mask
        //of the layer mask objectstodealdamage. Slightly over my head actually. This was
        //needed so we can check friend or foe before dealing damage. --Randy
        //int layerValue = (1 << col.gameObject.layer);
        //if ((objectsToDealDamage.value & layerValue) > 0)
        if ((objectsToDealDamage.value & 1 << col.gameObject.layer) == 1 << col.gameObject.layer)
        {
            col.SendMessage("TakeDamage", damageToDeal, SendMessageOptions.DontRequireReceiver);
            //Set these back to send messages because it was throwing errors trying to call
            //scripts (playerstatus) that weren't on things like the spikes, other enemies,
            //and walls. Having to do a test for this every time we want to call these
            //functions later down the line is a much bigger pain then having to rename a few
            //inline strings later down the line, especially since we know it's functions that
            //are extremely unlikely to change down the line.
            var knockbackAngle = -(transform.position - col.gameObject.transform.position);
            var knockbackAngle2D = new Vector2(knockbackAngle.x, knockbackAngle.y);
            knockbackAngle2D.Normalize();
            col.SendMessage("KnockBack", knockbackAngle2D, SendMessageOptions.DontRequireReceiver);
        }
	}
    
    public void StopAttacking()
    {
        SendMessageUpwards("ResumeControl", SendMessageOptions.DontRequireReceiver);
    }
}
