using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {

	int[] inputCoords;
	public Sprite[] endOfLevelSplashes;
	public Image endOfLevelImage;
	Canvas gameRecap;

	// Use this for initialization
	void Start () {
		inputCoords = new int[3];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowEndOfLevelSplash(bool win, int score) {
		Debug.Log("End of level splash");
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
