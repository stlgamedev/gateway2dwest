using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour {
    [Range(1,4)]
    public int playerId = 1;

    public GameObject characterPanel;
    public Vector3 selectorOffset = new Vector3(0, 0, 0);

    public GameObject[] playerSelectors;
    public SpriteRenderer[] playerSelectionSprites;
    private List<GameObject> selectionObjects = new List<GameObject>();
    public InputHelper[] playerInputHelpers;
    private InputHelper[] globalInputHelpers = new InputHelper[4];
    public GameObject[] selectablePlayerObjects;
    public GameObject[] readyObjects;
    public GameObject[] pressStartObjects;

    private int[] selectionIndex = new int[4];
    private bool[] playerReady = new bool[4];
    private bool[] isPlaying = new bool[4];


    // Use this for initialization
    void Start () {
        for (int i = 0; i < GameManager.instance.numberOfPlayers; i++)
        {
            isPlaying[i] = true;
            playerReady[i] = false;
        }
	    for(int i = 0; i < selectablePlayerObjects.Length; i++)
        {
            GameObject go = (GameObject)GameObject.Instantiate<GameObject>(selectablePlayerObjects[i]);
            Destroy(go.GetComponent<PlayerMovement>());
            Destroy(go.GetComponent<Status>());
            Destroy(go.GetComponent<BoxCollider2D>());
            Destroy(go.GetComponent<Animator>());
            Destroy(go.GetComponent<Rigidbody2D>());
            selectionObjects.Add(go);
            Vector3[] corners = new Vector3[4];
            characterPanel.GetComponent<RectTransform>().GetWorldCorners(corners);
            corners[1] = new Vector3(corners[1].x, corners[1].y, 0);
            go.transform.position = corners[1] + new Vector3((i+1.25f) + (4*-Mathf.Floor(i / 4)) , -(1.5f * Mathf.Floor(i / 4)),0) + new Vector3(0,-2.5f,0);
            if(i < 4)
            {
                globalInputHelpers[i] = playerInputHelpers[i];
                playerSelectors[i].transform.position = go.transform.position;
                selectionIndex[i] = i;
            }
        }
	}

    // Update is called once per frame
    void Update() {

        bool allReady = true;
        for (int i = 0; i < selectionIndex.Length; i++)
        {
            if (!playerReady[i] && isPlaying[i])
            {
                allReady = false;
                if (playerInputHelpers[i].GetRightDown())
                {
                    selectionIndex[i] += 1;
                    if (selectionIndex[i] >= selectionObjects.Count)
                    {
                        selectionIndex[i] -= selectionObjects.Count;
                    }
                }
                if (playerInputHelpers[i].GetLeftDown())
                {
                    selectionIndex[i] -= 1;
                    if (selectionIndex[i] < 0)
                    {
                        selectionIndex[i] += selectionObjects.Count;
                    }
                }

                if (playerInputHelpers[i].GetDownDown())
                {
                    selectionIndex[i] += 4;
                    if (selectionIndex[i] >= selectionObjects.Count)
                    {
                        selectionIndex[i] -= selectionObjects.Count + (selectionObjects.Count % 4);
                        if (selectionIndex[i] < 0)
                        {
                            selectionIndex[i] += 4;
                        }
                    }
                }
                if (playerInputHelpers[i].GetUpDown())
                {
                    selectionIndex[i] -= 4;
                    if (selectionIndex[i] < 0)
                    {
                        selectionIndex[i] += selectionObjects.Count + (selectionObjects.Count % 4);
                        if (selectionIndex[i] > selectionObjects.Count-1)
                        {
                            selectionIndex[i] -= 4;
                        }
                    }
                }


                if(playerInputHelpers[i].GetAttackDown())
                {
                    playerReady[i] = true;
                    GameManager.instance.playerSprites[i] = selectablePlayerObjects[selectionIndex[i]];
                    GameManager.instance.playerSprites[i].GetComponent<PlayerMovement>().inputHelper = playerInputHelpers[i];
                }

                playerSelectionSprites[i].GetComponent<SpriteRenderer>().sprite = selectionObjects[selectionIndex[i]].GetComponent<SpriteRenderer>().sprite;
                playerSelectors[i].transform.position = selectionObjects[selectionIndex[i]].transform.position + selectorOffset;
            }
            else
            {
                if (playerInputHelpers[i].GetAttackDown())
                {
                    playerReady[i] = false;
                }
            }
            playerSelectors[i].SetActive(!playerReady[i]);

            if (isPlaying[i])
            {
                readyObjects[i].SetActive(playerReady[i]);
            }
            else
            {
                if(globalInputHelpers[i].GetAttackDown())
                {
                    GameManager.instance.numberOfPlayers += 1;
                    playerInputHelpers[i] = globalInputHelpers[i];
                    pressStartObjects[i].SetActive(false);
                    playerSelectors[i].SetActive(true);
                    isPlaying[i] = true;
                }
                readyObjects[i].SetActive(false);
                playerSelectors[i].SetActive(false);
            }
        }
        if(allReady)
        {
            GameManager.instance.shouldCreatePlayers = true;
            Application.LoadLevel("Randy's Level");
        }
	}
}
