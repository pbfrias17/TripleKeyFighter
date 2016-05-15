using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject lcObj;
	LevelController lc;

	public int currentLevel = 0;
	public bool stillPlaying;


	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start() {
		StartGame();
	}


	void StartGame() {
		LoadNextLevel();
	}

	void LoadNextLevel() {
		Debug.Log("Loading next level");
		SceneManager.LoadScene("Test");
	}
	
	void OnLevelWasLoaded(int levelNum) {
		Debug.Log("scene index " + levelNum.ToString() + " was loaded");
		//levelNum here is the scene's build index
		if (levelNum == 1) {
			lcObj = GameObject.Find("Level"); //LevelController obj
			lc = lcObj.GetComponent<LevelController>();
			lc.InitLevel(++currentLevel);
			stillPlaying = true;
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
