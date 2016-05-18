using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {

	public Text level;
	public Text inputCoords;
	public Text score;
	public Text combo;
	public Image comboTimer;
	public Sprite[] endOfLevelSplashes;
	public Image endOfLevelImage;
	Canvas gameRecap;

	private IEnumerator comboTimerCoroutine; //need a way to manually stop this

	// Use this for initialization
	void Start () {
		comboTimerCoroutine = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisplayLevelNumber(int levelNum) {
		level.text = levelNum.ToString();
	}


	public void AppendToInputCoords(int val) {
		if (val < 0) inputCoords.text = "";
		else inputCoords.text += (val + 1).ToString();
	}

	public void UpdateCombo(float comboVal) {
		combo.text = "x" + comboVal.ToString();
	}

	public void UpdateComboTimer(float duration, float timestamp) {
		comboTimerCoroutine = ComboTimerRoutine(duration, timestamp);
		StartCoroutine(comboTimerCoroutine);
	}

	public void ResetComboTimer() {
		if (comboTimerCoroutine != null) StopCoroutine(comboTimerCoroutine);
	}

	public IEnumerator ComboTimerRoutine(float duration, float timestamp) {
		int loops = (int)(duration * 60);
		for(int i = 0; i < loops; i++) {
			comboTimer.fillAmount = (duration - (Time.time - timestamp)) / duration;
			yield return new WaitForSeconds(1 / loops);
		}

		UpdateCombo(1);
		comboTimer.fillAmount = 1;
	}

	public void UpdateScore(int scoreVal) {
		score.text = scoreVal.ToString();
	}

	public void ShowEndOfLevelSplash(bool win, int score) {
		if(win) {
			//check score
			endOfLevelImage.sprite = endOfLevelSplashes[1];
			endOfLevelImage.transform.position += new Vector3(0, -2, 0);
		}
		else {
			endOfLevelImage.sprite = endOfLevelSplashes[0];
		}
		endOfLevelImage.color = new Color(1, 1, 1, 1);
	}
}
