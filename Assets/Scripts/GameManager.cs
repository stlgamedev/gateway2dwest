using UnityEngine;
using System.Collections;

using System.Collections.Generic;

//Allows us to use Lists. 
using UnityEngine.UI;					//Allows us to use UI.
	
public class GameManager : MonoBehaviour
{
    public bool shouldCreatePlayers = false;
    [Range(1,4)]
	public int numberOfPlayers = 4;
	public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.

    public int targetDoor = -1;
    public GameObject[] playerSprites = new GameObject[4];
    public GameObject[] playerGui = new GameObject[4];
    [HideInInspector]
    public GameObject[] players = new GameObject[4];
	private BoardManager boardScript;
	private int level = 1;									//Current level number, expressed in game as "Day 1".
	private bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.

	//Awake is always called before any Start functions
	void Awake ()
	{
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad (gameObject);

		//Check if instance already exists
		if (instance == null) {
				
			//if not, set instance to this
            instance = gameObject.GetComponent<GameManager>();
		}
			//If instance already exists and it's not this:
        else if (instance != gameObject.GetComponent<GameManager>())
        {
				
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);	
		}

		//Get a component reference to the attached BoardManager script
		boardScript = GetComponent<BoardManager> ();
			
		//Call the InitGame function to initialize the first level 
		InitGame ();
	}

    void CreatePlayers()
    {
        Transform[] objectsToFollow = new Transform[4];

        //Simplified player sprites into an array rather then 4 independent variables.
        //This should make iteration easier.
        for (int i = 0; i < numberOfPlayers; i++)
        {
            //only adds the player if the player has been set. Should allow for
            if (playerSprites != null && playerSprites.Length > 0 && playerSprites[i] != null && shouldCreatePlayers)
            {
                players[i] = Instantiate(playerSprites[i]);
                objectsToFollow[i] = players[i].transform;
                players[i].transform.parent = transform;
                players[i].GetComponent<Status>().playerID = i;
                players[i].GetComponent<Status>().GUI = playerGui[i];
                players[i].transform.localPosition = Vector3.zero;
                /*InputHelper ih = );
                ih.horizontalAxis = "Horizontal" + (i + 1);
                ih.verticalAxis = "Vertical" + (i + 1);
                ih.attackButton = "Player " + (i + 1) + " Attack";
                ih.transform.parent = players[i].transform;
                players[i].GetComponent<PlayerMovement>().inputHelper = ih;*/
            }
            if(!shouldCreatePlayers)
            {
                players[i].transform.localPosition = Vector3.zero;
            }
            players[i].GetComponent<Status>().ResetGUI();
        }

        for (int i = 0; i < 4; i++)
        {
            if (players[i] == null)
            {
                playerGui[i].SetActive(false);
            }
        }
        CameraFollow camera = Camera.main.GetComponent<CameraFollow>();
        camera.objectsToFollow = objectsToFollow;
        shouldCreatePlayers = false;
    }
		
	//This is called each time a scene is loaded.
	void OnLevelWasLoaded (int index)
	{
		//Add one to our level number.
		level++;
        //Call InitGame to initialize our level.
        Door[] doors = GameObject.FindObjectsOfType<Door>();
        foreach (Door d in doors)
        {
            if (d.id == targetDoor)
            {
                transform.position = d.transform.position + d.spawnOffset;
            }
        }
        for(int i = 0; i < numberOfPlayers; i ++)
        {
            players[i].transform.localPosition = Vector3.zero;
            //players[i].GetComponent<Status>().ResetGUI();
            //playerGui[i] = players[i].GetComponent<Status>().GUI;
        }
        InitGame ();
	}
		
	//Initializes the game for each level.
	void InitGame ()
	{
        if (shouldCreatePlayers)
        {
            CreatePlayers();
        }
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.SetupScene (level);
			
	}
		
	//Update is called every frame.
	void Update ()
	{
		//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
		if (doingSetup)
				
				//If any of these are true, return and do not start MoveEnemies.
			return;

	}
}

