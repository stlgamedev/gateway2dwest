using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public int id = -1;
    public int target = -1;
    public string targetLevelName;
    public Vector3 spawnOffset = new Vector3(0, -2, 0);


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
        handleCollision(col);
    }

    private void handleCollision(Collider2D col)
    {
        if(col.tag == "Player")
        {
            RememberActiveStatus.maxID = -1;
            Debug.Log("Loading level " + targetLevelName);
            GameManager.instance.targetDoor = target;
            Application.LoadLevel(targetLevelName);
        }
    }
}
