using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Status : MonoBehaviour {
    public int playerID = -1;
    public float hitPoints = 3;
    public float maxHitPoints = 3;
    public float money = 0;
    public Text moneyText;
    public GameObject GUI;
    
    public bool poisioned = false;

    public AudioClip damageSound;

	private bool canTakeDamage = true;
	
	Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
    
    public void ResetGUI()
    {
        if (GUI == null)
        {
            GUI = GameObject.Find("Player " + (playerID+1) + " GUI");
            GameManager.instance.playerGui[playerID] = GUI;
        }
        if (playerID >= 0 && GUI != null)
        {
            moneyText = GUI.transform.FindChild("Money").GetComponent<Text>();
            moneyText.text = "$" + money;
        }
        UpdateHearts();
    }

    void OnLevelWasLoaded(int index)
    {
        ResetGUI();
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

    public virtual void TakeDamage(float damageToDeal)// creates imposter to not throw errors.
    {
        if (canTakeDamage)
        {
            Debug.Log(gameObject.name + " is taking damage.");
            hitPoints -= damageToDeal; //apply damage
            hitPoints = Mathf.Clamp(hitPoints, 0, maxHitPoints);
            if (GUI != null)
            {
                //only check if it has a gui element. Maybe add healthbars to enemies?
                UpdateHearts(); 
            }
            canTakeDamage = false;
            Invoke("EnableDamage", .35f); //Allows us to take damage again
            Camera.main.GetComponent<CameraFollow>().ShakeCamera(.12f, .2f);
            SoundManager.instance.PlaySingle(damageSound);
            if(hitPoints == 0)
            {
                Die();
                hitPoints = -1;
            }
        }
    }

    public void UpdateHearts()
    {
        for (int i = 1; i <= maxHitPoints; i++)
        {
            if (i > hitPoints)
            {
                GUI.transform.FindChild("Heart " + i).gameObject.SetActive(false);
            }
            else
            {
                GUI.transform.FindChild("Heart " + i).gameObject.SetActive(true);
            }
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

    public void Die()
    {
        Collider[] cols = GetComponents<Collider>();
        for(int i = 0; i < cols.Length; i++)
        {
            Destroy(cols[i]);
        }
        foreach (Transform t in transform)
        {
            if (t != transform)
            {
                t.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
