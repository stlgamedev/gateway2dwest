using UnityEngine;
using System.Collections;

public class DestroyOnDeath : MonoBehaviour {
    public GameObject objectToDestroyOnDeath;
    public GameObject[] objectsToDisable;
    public void DestroyObject()
    {
        if (objectToDestroyOnDeath.GetComponent<RememberActiveStatus>() != null)
        {
            Debug.Log("Saves. Deactivating " + objectToDestroyOnDeath.name);
            objectToDestroyOnDeath.GetComponent<RememberActiveStatus>().SetInactive();
        }
        else
        {
            Debug.Log("Does not save. Destroying " + objectToDestroyOnDeath.name);
            Destroy(objectToDestroyOnDeath);
        }
    }
    
    public void Die()
    {
        foreach(GameObject go in objectsToDisable)
        {
            foreach (Renderer r in go.GetComponents<Renderer>())
            {
                r.enabled = false;
            }
            foreach (Collider c in go.GetComponents<Collider>())
            {
                c.enabled = false;
            }
            foreach (Collider2D c in go.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
        }
        GetComponent<Animator>().Play("Die");
    }
}
