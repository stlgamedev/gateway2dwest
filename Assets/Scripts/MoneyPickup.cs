using UnityEngine;
using System.Collections;

public class MoneyPickup : MonoBehaviour {
    public float moneyToGive = 5;
    public AudioClip pickupSound;
    // Use this for initialization
    void OnCollisionEnter2D(Collision2D col)
    {
        OnTriggerEnter2D(col.collider);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.BroadcastMessage("GiveMoney", moneyToGive, SendMessageOptions.DontRequireReceiver);
                //Send message to whoever picked up the money.
        
		SoundManager.instance.PlaySingle (pickupSound);

                //Play sound when picked up.
        Destroy(gameObject);
                //Destroy self after doing all that.
    }
}
