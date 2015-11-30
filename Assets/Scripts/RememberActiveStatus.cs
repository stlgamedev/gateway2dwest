using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RememberActiveStatus : MonoBehaviour {
    public int instanceID = -1;
    public static int maxID = -1;

    void Start()
    {
        gameObject.SetActive(bool.Parse(PlayerPrefs.GetString(Application.loadedLevel + "-" + instanceID.ToString(), "true")));
    }

    void OnLevelWasLoaded(int levelID)
    {
        gameObject.SetActive(bool.Parse(PlayerPrefs.GetString(Application.loadedLevel + "-" + instanceID.ToString(), "true")));
    }

    public void GenerateInstanceIDs()
    {
        maxID += 1;
        if (instanceID == -1)
        {
            bool unsetIdFound = false;
            while (!unsetIdFound)
            {
                RememberActiveStatus[] objList = GameObject.FindObjectsOfType<RememberActiveStatus>();
                foreach (RememberActiveStatus obj in objList)
                {
                    if (obj.instanceID == maxID)
                    {
                        maxID += 1;
                    }
                    else
                    {
                        unsetIdFound = true;
                    }
                }
            }
            instanceID = maxID;
        }
    }

    public void SetInactive()
    {
        PlayerPrefs.SetString(Application.loadedLevel + "-" + instanceID, "false");
        gameObject.SetActive(false);
    }
}

#if UNITY_EDITOR
public class GenerateGuids : ScriptableWizard
{
    [MenuItem("Window/CM Tools/Generate Guids for RememberActiveStatus")]
    static void GenerateUniqueIDs()
    {
        RememberActiveStatus[] objList = GameObject.FindObjectsOfType<RememberActiveStatus>();
        RememberActiveStatus.maxID = -1;
        foreach (RememberActiveStatus obj in objList)
        {
            obj.GenerateInstanceIDs();
            EditorUtility.SetDirty(obj);
        }
    }

    [MenuItem("Window/CM Tools/Reset Player Prefs")]
    static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
#endif