using UnityEngine;
using System.Collections;

public class PlusTimeItem : MonoBehaviour {

	public float timeAdded;
	GameObject lcObj;
	LevelController lc;


	// Use this for initialization
	void Start () {
		lcObj = GameObject.Find("Level");
		lc = lcObj.GetComponent<LevelController>();
	}
	
	public void PickUp() {
		Debug.Log("adding time");
		lc.remTime += timeAdded;
		Destroy(gameObject);
	}
}
