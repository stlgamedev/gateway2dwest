using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public float hitPoints = 100;
    public float money = 0;

	public bool poisioned = false;
    public bool takingDamage = false;

    public Vector3 attackerPos;

    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (poisioned) {
			hitPoints -= Time.deltaTime * 5.0f;
		}

        if(takingDamage)
        {
            rend.material.color = Color.red;
        }
        else
        {
            rend.material.color = Color.white;
        }
	}

    public void TakeDamage(CollisionData cd)
    {
        if (!takingDamage)
        {
            hitPoints -= cd.damageToDeal;
            attackerPos = cd.sender.transform.position;
            takingDamage = true;
            Invoke("UnlockControls", .08f);
        }
    }

    public void UnlockControls()
    {
        takingDamage = false;
    }

    public void GiveMoney(float moneyToGive)
    {
        money += moneyToGive;
    }
}
