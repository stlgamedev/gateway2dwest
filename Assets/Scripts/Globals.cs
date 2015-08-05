using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour {
    public static List<GameObject> Players = new List<GameObject>();  //is a list for multiplayer support if we want to add it.
                                                        //If single player we can still just get Players(0).
}
