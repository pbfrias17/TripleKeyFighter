using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject lcObj;
	LevelController lc;
	public GameObject playerObj;
	PlayerController pc;

	public int currentLevel;
	public bool stillPlaying;
	public bool levelLoaded;

	public int gameScore;

	AudioSource bgMusic;
	bool musicPlaying;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		levelLoaded = false;
	}

	void Start() {
		bgMusic = GetComponent<AudioSource>();
	}

	//invoked by "Play" button
	public void StartGame() {
		LoadNextLevel();
		gameScore = 0;
		currentLevel = 0;
		musicPlaying = false;
		stillPlaying = true;
	}

	void LoadNextLevel() {
		SceneManager.LoadScene("LevelScene");
	}
	
	void OnLevelWasLoaded(int levelNum) {
		PlayMusic();

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

	void PlayMusic() {
		if (!musicPlaying) {
			musicPlaying = true;
			bgMusic.Play();
		}
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
				}
			}

			if(!stillPlaying) {
				playerObj.GetComponent<PlayerController>().canTakeInput = false;
				//show recap screen
				if (lc.newGame) {
					StartGame();
				}
			}
		}
	}
	
}
