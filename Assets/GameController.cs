using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject lcObj;
	LevelController lc;
	public GameObject playerObj;
	PlayerController pc;

	public int currentLevel = 0;
	public bool stillPlaying;
	public bool levelLoaded;


	void Awake() {
		levelLoaded = false;
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
		SceneManager.LoadScene("LevelScene");
	}
	
	void OnLevelWasLoaded(int levelNum) {
		Debug.Log("scene index " + levelNum.ToString() + " was loaded");
		//levelNum here is the scene's build index
		if (levelNum == 1) {
			playerObj = GameObject.Find("Player");
			lcObj = GameObject.Find("Level"); //LevelController obj
			lc = lcObj.GetComponent<LevelController>();
			lc.InitLevel(++currentLevel);
			stillPlaying = true;
		}
		levelLoaded = true;
	}

	void Update () {
		if(levelLoaded) {
			if(stillPlaying) {
				if (lc.levelWon) {
					//game won
					stillPlaying = false;
					Debug.Log("GC: Level Won!");
					LoadNextLevel();
				}

				if(lc.levelLost) {
					stillPlaying = false;
					Debug.Log("GC: Game Over");
					//show recap screen
				}
			}

			if(!stillPlaying) {
				playerObj.GetComponent<PlayerController>().canTakeInput = false;
			}
		}
	}
	
}
