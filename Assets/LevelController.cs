using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	public int enemiesAmt, hazardsAmt;
	public GameObject spawnerObj;
	SpawnController spawner;

	public Text timerText;
	public float maxTime;
	public float remTime;
	float timeOfLastKill;

	public bool timerOver = true;
	public bool stillPlaying = true;
	public bool levelWon = false;
	public bool levelLost = false;

	public GameObject HUDObj;
	public int score;
    public float comboThreshold;
    public float comboResetTime;
    public float currComboVal;
    public float comboStepVal;

	void Start () {
		Camera.main.orthographicSize = 15;
	}

	public void InitLevel(int levelNum) {
		Debug.Log("Initiating Level " + levelNum.ToString());
		InitDifficulty(levelNum);
		spawner = spawnerObj.GetComponent<SpawnController>();
		spawner.Build(enemiesAmt, hazardsAmt);
		spawner.SpawnAll();
		remTime = maxTime;
		timeOfLastKill = maxTime;
		HUDObj.GetComponent<HUDController>().UpdateScore(score); //update score at start
		timerOver = false;
	}

	void InitDifficulty(int levelNum) {
		enemiesAmt = Mathf.Max(levelNum, 3);
		enemiesAmt = Mathf.Min(enemiesAmt, 15); // min/max of 3/15 enemies per level
		hazardsAmt = levelNum * 2;
		if((hazardsAmt + enemiesAmt) > 27) {
			hazardsAmt -= (hazardsAmt + enemiesAmt - 27);
		}

        //game difficulty/time hits a plateau - which is fine
		maxTime = Mathf.Max((float) (25 - (levelNum * 1.2)), 15);

	}

	void Update () {
		if(!timerOver) {
			remTime -= Time.deltaTime;
			timerText.text = remTime.ToString("F2");
		}

		//GameOver when timer reaches 0
		if(remTime <= 0 && !timerOver) {
			GameOver();
		}
	}

	//message to be received from enemy deaths
	public void ReceiveDeathMessage(int enemiesLeft) {
		//update score after each kill
		UpdateScore(remTime);

		//check win condition
		if(enemiesLeft == 0 && !timerOver) {
			GameWin();
		}
	}

	//score is influenced by player timing
	void UpdateScore(float timeOfKill) {
		float diff = timeOfLastKill - timeOfKill;
		int nextScore = (int) (100 / diff) + 100; //kill = 100 base score
        /*if(diff <= comboThreshold) {
            //combo multiplier!
        }*/
		Debug.Log("diff: " + diff.ToString() + " -> score: " + nextScore.ToString());
		score += nextScore;
		HUDObj.GetComponent<HUDController>().UpdateScore(score);
		timeOfLastKill = timeOfKill;
	}

	//message to be received from player input
	public void ReceivePlayerInput(int inputVal) {
		HUDObj.GetComponent<HUDController>().AppendToInputCoords(inputVal);
	}


	void GameWin() {
		timerOver = true;
		stillPlaying = false;
		HUDObj.GetComponent<HUDController>().ShowEndOfLevelSplash(true, 100);
		StartCoroutine(InterLevelWait(3));
	}

	public IEnumerator InterLevelWait(float waitDur) {
		yield return new WaitForSeconds(waitDur);
		levelWon = true;
	}

	void GameOver() {
		stillPlaying = false;
		timerOver = true;
		levelLost = true;
		timerText.text = "0.00";
		HUDObj.GetComponent<HUDController>().ShowEndOfLevelSplash(false, 100);
	}
}
