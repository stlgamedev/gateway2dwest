using UnityEngine;
using System.Collections;

public class MoneyPickup : MonoBehaviour {
    public float moneyToGive = 5;
    // Use this for initialization
    void OnCollisionEnter2D(Collision2D col)
    {
        OnTriggerEnter2D(col.collider);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.root.SendMessage("GiveMoney", moneyToGive, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
