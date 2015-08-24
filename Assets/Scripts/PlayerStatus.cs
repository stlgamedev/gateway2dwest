using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    public float hitPoints = 100;
    public float money = 0;
    
    public bool poisioned = false;

    public AudioClip damageSound;

	public Text moneyUIObject;

	private bool canTakeDamage = true;
	
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
            Camera.main.GetComponent<AudioSource>().PlayOneShot(damageSound);
        }
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
