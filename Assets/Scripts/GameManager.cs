using UnityEngine;
using System.Collections;

using System.Collections.Generic;

//Allows us to use Lists. 
using UnityEngine.UI;					//Allows us to use UI.
	
public class GameManager : MonoBehaviour
{
    [Range(1,4)]
	public int numberOfPlayers = 4;
	public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.

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

        Transform[] objectsToFollow = new Transform[4];

        //Simplified player sprites into an array rather then 4 independent variables.
        //This should make iteration easier.
        for (int i = 0; i < numberOfPlayers; i++)
        {
            //only adds the player if the player has been set. Should allow for
            if (playerSprites != null && playerSprites.Length > 0 && playerSprites[i] != null)
            {
                players[i] = Instantiate(playerSprites[i]);
                objectsToFollow[i] = players[i].transform;
                players[i].transform.parent = transform;
                players[i].GetComponent<Status>().playerID = i;
                players[i].GetComponent<Status>().GUI = playerGui[i];
            }
        }

        CameraFollow camera = Camera.main.GetComponent<CameraFollow>();
		camera.objectsToFollow = objectsToFollow;
	

		//Get a component reference to the attached BoardManager script
		boardScript = GetComponent<BoardManager> ();
			
		//Call the InitGame function to initialize the first level 
		InitGame ();
	}
		
	//This is called each time a scene is loaded.
	void OnLevelWasLoaded (int index)
	{
		//Add one to our level number.
		level++;
		//Call InitGame to initialize our level.
		InitGame ();
	}
		
	//Initializes the game for each level.
	void InitGame ()
	{
			
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

