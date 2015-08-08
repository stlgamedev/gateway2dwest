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
        col.transform.root.BroadcastMessage("GiveMoney", moneyToGive, SendMessageOptions.DontRequireReceiver);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(pickupSound);
        Destroy(gameObject);
    }
}
