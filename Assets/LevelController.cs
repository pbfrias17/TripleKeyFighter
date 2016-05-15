using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	public int enemiesAmt, hazardsAmt;
	public GameObject spawnerObj;
	SpawnController spawner;

	public Text timerText;
	public float maxTime;
	public float remTime;
	public bool timerOver = false;

	public bool stillPlaying = true;
	public bool levelWon = false;

	void Start () {
		Camera.main.orthographicSize = 15;
		spawner = spawnerObj.GetComponent<SpawnController>();
		spawner.Build(enemiesAmt, hazardsAmt);
		spawner.SpawnAll();
		remTime = maxTime;
	}

	void Update () {
		if(!timerOver) {
			remTime -= Time.deltaTime;
			timerText.text = remTime.ToString("F2");
		}

		if(remTime <= 0 && !timerOver) {
			GameOver();
		}
	}

	//message to be received from enemy deaths
	public void ReceiveDeathMessage(int enemiesLeft) {
		Debug.Log("enemies left = " + enemiesLeft);
		if(enemiesLeft == 0 && !timerOver) {
			GameWin();
		}
	}

	void GameWin() {
		timerOver = true;
		stillPlaying = false;
		levelWon = true;
	}

	void GameOver() {
		stillPlaying = false;
		timerOver = true;
		levelWon = false;
		timerText.text = "0.00";
	}
}
