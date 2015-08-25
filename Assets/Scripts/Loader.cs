using UnityEngine;
using System.Collections;

namespace Completed
{	
	public class Loader : MonoBehaviour 
	{
		public GameManager gameManager;			//GameManager prefab to instantiate.
		public SoundManager soundManager;
		
		void Awake ()
		{
			//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
			if (GameManager.instance == null) {
				
				//Instantiate gameManager prefab
				Instantiate (gameManager.gameObject);

			}
			
			//Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
			if (SoundManager.instance == null) {
				
				//Instantiate SoundManager prefab
				Instantiate (soundManager.gameObject);
			}
		}
	}
}