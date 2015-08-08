using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour {
    public static List<GameObject> Players = new List<GameObject>(); //Setting foundation for multiple players
    public static AudioSource PlaySound;
    public void Start()
    {
        PlaySound = Camera.main.GetComponent<AudioSource>();
    }
}

public struct CollisionData
{
    public float damageToDeal;
    public GameObject sender;

    public CollisionData(float DamageToDeal, GameObject go)
    {
        damageToDeal = DamageToDeal;
        sender = go;
    }
}