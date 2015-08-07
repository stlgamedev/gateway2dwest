using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
    public float damageToDeal = 1;
	// Use this for initialization
    void OnCollisionEnter2D(Collision2D col)
    {
        OnTriggerEnter2D(col.collider);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.root.SendMessage("TakeDamage",new CollisionData(damageToDeal,gameObject), SendMessageOptions.DontRequireReceiver);
    }
}
