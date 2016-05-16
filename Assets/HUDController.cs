using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {

	public Text inputCoords;
	public Text score;
	public Sprite[] endOfLevelSplashes;
	public Image endOfLevelImage;
	Canvas gameRecap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AppendToInputCoords(int val) {
		if (val < 0) inputCoords.text = "";
		else inputCoords.text += (val + 1).ToString();
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
