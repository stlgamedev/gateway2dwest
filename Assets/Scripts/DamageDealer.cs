using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
    public float damageToDeal = 1;
	// Use this for initialization

    void OnCollisionStay2D(Collision2D col)
    {
        OnTriggerEnter2D(col.collider);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        OnTriggerEnter2D(col.collider);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.BroadcastMessage("TakeDamage",new CollisionData(damageToDeal,gameObject), SendMessageOptions.DontRequireReceiver);
    }
}
