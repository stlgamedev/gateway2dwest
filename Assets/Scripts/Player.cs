using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	void Start () 
    {
        Globals.Players.Add(gameObject);//Make it easier for enemies and other scripts to find players.
	}
	
	void Update () 
    {
	
	}
}
