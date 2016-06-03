using UnityEngine;
using System.Collections;


public class Block : MonoBehaviour {

	public Sprite[] sprites;

    public GameObject enemyObj;
    public GameObject hazardObj;
	public GameObject itemObj;
	public bool isEmpty;
    public bool playerLanded;
    //public Vector3 spawnOffset;

	void Awake() {
		isEmpty = true;
	}

	// Use this for initialization
	void Start () {
        //isEmpty = true;
		//gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 5)];
	}
	
	public bool IsEmpty() {
		return isEmpty;
	}

}
