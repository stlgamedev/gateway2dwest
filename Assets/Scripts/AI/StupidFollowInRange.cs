using UnityEngine;
using System.Collections;

public class StupidFollowInRange : MonoBehaviour
{
    //Called stupid cause it's just a direct go toward player.
    //Will probably add a smart follow later that will pathfind.
    //That will take considerablly large amount more effort
    //to get working right, thus stupid follow was made.
    public float range = 4;
    public float speed = 3;
    public bool needsLineOfSight = false;
    bool isFollowing = false;
    Rigidbody2D rb;
    GameObject followingObject;
    Status status;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && status.hitPoints > 0)
        {
            if (!isFollowing)
            {
                rb.velocity = rb.velocity * .98f;
                foreach (GameObject player in GameManager.instance.players)
                {
                    if (player != null)
                    {
                        if (Vector2.Distance(player.transform.position, transform.position) <= range)
                        {
                            isFollowing = true;
                            followingObject = player;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (Vector2.Distance(followingObject.transform.position, transform.position) >= range)
                {
                    isFollowing = false;
                }
                Vector2 direction = (followingObject.transform.position - transform.position).normalized;
                rb.velocity += (direction * speed)*Time.deltaTime;
            }
        }
    }
}
