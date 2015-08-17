using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {

    public float damageToDeal = 1;

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
		col.GetComponent<PlayerStatus> ().TakeDamage (damageToDeal);
		var knockbackAngle = -(transform.position - col.gameObject.transform.position).normalized;
		knockbackAngle.Normalize ();
		col.GetComponent<PlayerMovement> ().KnockBack (knockbackAngle);
	}
}
