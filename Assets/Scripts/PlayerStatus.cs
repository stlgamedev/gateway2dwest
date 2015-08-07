using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    public int playerID = 0; //Setting foundation for multiple players
	public float hitPoints = 100;
    public float money = 0;
    
    public bool poisioned = false;
    public bool takingDamage = false;
    public bool canTakeDamage = true;

    public Text moneyUIObject;

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
            rend.material.color = Color.red; //Changes color to red when taking damage
        }
        else
        {
            rend.material.color = Color.white; //Sets color to white when not taking damage
        }
	}

    public void TakeDamage(CollisionData cd)
    {
        if (canTakeDamage)
        {
            hitPoints -= cd.damageToDeal; //apply damage
            attackerPos = cd.sender.transform.position; //set the position of the attacker so we can perform knockback
            takingDamage = true; //sets taking damage flag to true
            canTakeDamage = false;
            Invoke("UnlockControls", .08f); //resets taking damage flag
            Invoke("EnableDamage", .35f); //Allows us to take damage again
            Camera.main.GetComponent<CameraFollow>().ShakeCamera(.08f, .1f);
        }
    }

    public void UnlockControls()
    {
        takingDamage = false;
    }

    public void EnableDamage()
    {
        canTakeDamage = true;
    }

    public void GiveMoney(float moneyToGive)
    {
        money += moneyToGive;
        moneyUIObject.text = "$" + money.ToString();
    }
}
