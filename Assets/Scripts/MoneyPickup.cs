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
        if (LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            //Send message to whoever picked up the money.
            col.transform.BroadcastMessage("GiveMoney", moneyToGive, SendMessageOptions.DontRequireReceiver);
            

            //Play sound when picked up.
            SoundManager.instance.PlaySingle(pickupSound);


            //Setting to inactive state, so if we come back to the level, it will be pickedup still.
            BroadcastMessage("SetInactive");
            
        }
    }
}
