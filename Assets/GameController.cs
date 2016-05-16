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

	public int gameScore;


	void Awake() {
		levelLoaded = false;
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start() {
		StartGame();
	}


	void StartGame() {
		LoadNextLevel();
		gameScore = 0;
	}

	void LoadNextLevel() {
		SceneManager.LoadScene("LevelScene");
	}
	
	void OnLevelWasLoaded(int levelNum) {
		//levelNum here is the scene's build index
		if (levelNum == 1) {
			playerObj = GameObject.Find("Player");
			lcObj = GameObject.Find("Level"); //LevelController obj
			lc = lcObj.GetComponent<LevelController>();
			lc.score = gameScore;
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
					gameScore = lc.score;
					LoadNextLevel();
				}

				if(lc.levelLost) {
					stillPlaying = false;
					Debug.Log("GC: Game Over");
					gameScore = lc.score;
					//show recap screen
				}
			}

			if(!stillPlaying) {
				playerObj.GetComponent<PlayerController>().canTakeInput = false;
			}
		}
	}
	
}
