using UnityEngine;
using System;
using System.Collections.Generic;
//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.
using UnityEditor;

	
public class BoardManager : MonoBehaviour
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count
	{
		public int minimum; 			//Minimum value for our Count class.
		public int maximum; 			//Maximum value for our Count class.
			
		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	public int horizontalRoomCount = 3;
	public int verticalRoomCount = 5;

	public int Columns {get {return columns;}}
	public int Rows {get {return rows;}}

	private int columns = 16; 	//Number of columns in our game board.
	private int rows = 10;		//Number of rows in our game board.

	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	private List <Vector2> gridPositions = new List <Vector2> ();	//A list of possible locations to place tiles.
		
		
//	//Clears our list gridPositions and prepares it to generate a new board.
//	void InitialiseList ()
//	{
//		//Clear our list gridPositions.
//		gridPositions.Clear ();
//			
//		//Loop through x axis (columns).
//		for (int x = 1; x < columns-1; x++) {
//			//Within each column, loop through y axis (rows).
//			for (int y = 1; y < rows-1; y++) {
//				//At each index add a new Vector2 to our list with the x and y coordinates of that position.
//				gridPositions.Add (new Vector2 (x, y));
//			}
//		}
//	}

	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;
		var rooms = new GameObject [horizontalRoomCount, verticalRoomCount];

		var tileCount = new Vector2 (columns, rows);
		for (int i = 0; i<rooms.GetLength(0); i++) {
			for (int j = 0; j<rooms.GetLength(1);j++) {
				var newRoom = RandomRoom();
				newRoom.transform.position = newRoom.transform.position + new Vector3(i * tileCount.x, j * tileCount.y, 0);
				rooms[i,j] = newRoom;
			}
		}


		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
//			for(int x = -1; x < columns + 1; x++)
//			{
//				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
//				for(int y = -1; y < rows + 1; y++)
//				{
//					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
//					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
//					
//					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
//					if(x == -1 || x == columns || y == -1 || y == rows)
//						toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
//					
//					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector2 corresponding to current grid position in loop, cast it to GameObject.
//					GameObject instance =
//						Instantiate (toInstantiate, new Vector2 (x, y), Quaternion.identity) as GameObject;
//					
//					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
//					instance.transform.SetParent (boardHolder);
//				}
//			}
	}
		
		
	//RandomPosition returns a random position from our list gridPositions.
	Vector2 RandomPosition ()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (0, gridPositions.Count);
			
		//Declare a variable of type Vector2 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector2 randomPosition = gridPositions [randomIndex];
			
		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt (randomIndex);
			
		//Return the randomly selected Vector2 position.
		return randomPosition;
	}
		
		
	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range (minimum, maximum + 1);
			
		//Instantiate objects until the randomly chosen limit objectCount is reached
		for (int i = 0; i < objectCount; i++) {
			//Choose a position for randomPosition by getting a random position from our list of available Vector2s stored in gridPosition
			Vector2 randomPosition = RandomPosition ();
				
			//Choose a random tile from tileArray and assign it to tileChoice
			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];
				
			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}
		
		
	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		//Creates the outer walls and floor.
		BoardSetup ();
			
//			//Reset our list of gridpositions.
//			InitialiseList ();
//			
//			//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
//			LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
//			
//			//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
//			LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
//			
//			//Determine number of enemies based on current level number, based on a logarithmic progression
//			int enemyCount = (int)Mathf.Log(level, 2f);
//			
//			//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
//			LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
//			
//			//Instantiate the exit tile in the upper right hand corner of our game board
//			Instantiate (exit, new Vector2 (columns - 1, rows - 1), Quaternion.identity);
	}

	private GameObject RandomRoom () {	
		var randomTileset = Random.Range (1, 4);
		var variableForPrefab = (GameObject)Resources.Load("LevelComponents/SampleField"+randomTileset, typeof(GameObject));
		
		var instance = Instantiate(variableForPrefab,
		                           new Vector3(-0.5f, 0.33f, 0.0f),
		                           Quaternion.identity) as GameObject;
		instance.transform.localScale = Vector3.one;
		instance.transform.SetParent(boardHolder);
		return instance;
	}

}
