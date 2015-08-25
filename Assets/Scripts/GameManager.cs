using UnityEngine;
using System.Collections;

namespace Completed
{
	using System.Collections.Generic;		//Allows us to use Lists. 
	using UnityEngine.UI;					//Allows us to use UI.
	
	public class GameManager : MonoBehaviour
	{
		public int numberOfPlayers = 4;

		public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.

		public GameObject player1Sprite;
		public GameObject player2Sprite;
		public GameObject player3Sprite;
		public GameObject player4Sprite;

		private BoardManager boardScript;						//Store a reference to our BoardManager which will set up the level.
		private int level = 1;									//Current level number, expressed in game as "Day 1".
		private bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.
		
		//Awake is always called before any Start functions
		void Awake()
		{
			//Check if instance already exists
			if (instance == null) {
				
				//if not, set instance to this
				instance = this;
			}
			//If instance already exists and it's not this:
			else if (instance != this) {
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy (gameObject);	
			}

			List<GameObject> players = new List<GameObject> ();

			players.Add(Instantiate(player1Sprite));
			players.Add(Instantiate(player2Sprite));
			players.Add(Instantiate(player3Sprite));
			players.Add(Instantiate(player4Sprite));

			var canvas = FindObjectOfType<Canvas> ();

			List<Transform> objectsToFollow = new List<Transform> ();
			foreach (GameObject player in players) {
				objectsToFollow.Add(player.transform);
				var status = player.GetComponent<PlayerStatus>();
				var moneyUI = canvas.GetComponentInChildren<Text>();
				status.moneyUIObject = moneyUI;
				player.transform.parent = transform;
			}

			for (int i = 3; i >= numberOfPlayers; i--) {
				var player = players[i];
				objectsToFollow.Remove(player.transform);
				players.Remove(player);
				Destroy(player);
			}

			CameraFollow camera = (CameraFollow)FindObjectOfType (typeof(CameraFollow));
			camera.objectsToFollow = objectsToFollow;

			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);

			//Get a component reference to the attached BoardManager script
			boardScript = GetComponent<BoardManager>();
			
			//Call the InitGame function to initialize the first level 
			InitGame();
		}
		
		//This is called each time a scene is loaded.
		void OnLevelWasLoaded(int index)
		{
			//Add one to our level number.
			level++;
			//Call InitGame to initialize our level.
			InitGame();
		}
		
		//Initializes the game for each level.
		void InitGame()
		{
			
			//Call the SetupScene function of the BoardManager script, pass it current level number.
			boardScript.SetupScene(level);
			
		}
		
		//Update is called every frame.
		void Update()
		{
			//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
			if(doingSetup)
				
				//If any of these are true, return and do not start MoveEnemies.
				return;

		}

	}
}

