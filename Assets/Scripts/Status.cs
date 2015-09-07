using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Status : MonoBehaviour {
    public int playerID = -1;
    public float hitPoints = 100;
    public float money = 0;
    public Text moneyText;
    
    public bool poisioned = false;

    public AudioClip damageSound;

	private bool canTakeDamage = true;
	
	Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        if (playerID >= 0)
        {
            moneyText = GameObject.Find("Player " + (playerID + 1) + " GUI").transform.FindChild("Money").GetComponent<Text>();
        }
        //This works... but is really really clunky and I don't like it. Need to find a better way. Maybe an array on gamemanager?
	}
	
	// Update is called once per frame
	void Update () {
		if (poisioned) {
			hitPoints -= Time.deltaTime * 5.0f;
		}

        if(!canTakeDamage)
        {
            rend.material.color = Color.red; //Changes color to red when taking damage
        }
        else
        {
            rend.material.color = Color.white; //Sets color to white when not taking damage
        }
	}

    public virtual void TakeDamage(float damageToDeal)
    {
        if (canTakeDamage)
        {
            hitPoints -= damageToDeal; //apply damage
            canTakeDamage = false;
            Invoke("EnableDamage", .35f); //Allows us to take damage again
            Camera.main.GetComponent<CameraFollow>().ShakeCamera(.12f, .2f);
            SoundManager.instance.PlaySingle(damageSound);
        }
    }

    public void EnableDamage()
    {
        canTakeDamage = true;
    }

    public void GiveMoney(float moneyToGive)
    {
        money += moneyToGive;
		moneyText.text = "$" + money;
    }
}
