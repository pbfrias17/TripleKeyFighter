using UnityEngine;
using System.Collections;


public class Block : MonoBehaviour {

	public Sprite[] sprites;

    public GameObject enemyObj;
    public GameObject hazardObj;
    public bool isEmpty;
    public bool playerLanded;
    //public Vector3 spawnOffset;

	// Use this for initialization
	void Start () {
        isEmpty = true;
		//gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 5)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
