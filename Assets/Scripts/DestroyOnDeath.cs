using UnityEngine;
using System.Collections;

public class DestroyOnDeath : MonoBehaviour {
    public GameObject objectToDestroyOnDeath;
    public GameObject[] objectsToDisable;
    public void DestroyObject()
    {
        Debug.Log("Destroying " + objectToDestroyOnDeath.name);
        Destroy(objectToDestroyOnDeath);
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
