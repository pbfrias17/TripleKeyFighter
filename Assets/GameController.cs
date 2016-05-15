using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject lcObj;
	LevelController lc;

	public int currentLevel = 1;
	public bool stillPlaying;


	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		stillPlaying = true;
		lc = lcObj.GetComponent<LevelController>();
	}

	void LoadNextLevel() {
		SceneManager.LoadScene("Test");
	}
	
	void OnLevelWasLoaded(int level) {
		currentLevel++;
		if (level == 1) {
			stillPlaying = true;
			lcObj = GameObject.Find("Level");
			lc = lcObj.GetComponent<LevelController>();

			
		}
	}

	void Update () {
		if(stillPlaying) {
			if (lc.levelWon) {
				//game won
				stillPlaying = false;
				Debug.Log("GC: Level Won!");
				LoadNextLevel();
			}
		}
	}
	
}
