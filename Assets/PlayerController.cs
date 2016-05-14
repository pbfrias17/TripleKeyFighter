using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Animator playerAnim;
	SpriteRenderer playerSR;
	Sprite playerSprite;
	public Sprite[] sprites;
	public Sprite testSprite;
	int currAttackSet;
	int attackCommandLen = 3;
	int[] attackCommand;
	int attackCommandAmt = 0;
	float playerDepth;
	GameObject curr9Block;

	AudioSource playerAudio;
	public AudioClip[] phase1Audio;
	public AudioClip[] phase2Audio;
	public AudioClip[] phase3Audio;

    public int damageDealt;

	// Use this for initialization
	void Start () {
		playerAnim = gameObject.GetComponent<Animator>();
		playerSR = gameObject.GetComponent<SpriteRenderer>();
		gameObject.GetComponent<SpriteRenderer>().sprite = testSprite;

		playerAudio = gameObject.GetComponent<AudioSource>();
		attackCommand = new int[attackCommandLen];
		playerDepth = gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("New State")) { //||
			//(playerAnim.IsInTransition(0) && playerAnim.GetNextAnimatorStateInfo(0).IsName("New State"))) {
			if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) ||
				Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
				if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.LeftArrow))
					HandleAttack(0);
				else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.DownArrow))
					HandleAttack(1);
				else
					HandleAttack(2);
			}
		}

		if (Input.GetMouseButton(0))
			playerAudio.Play();

	}

	void HandleAttack(int val) {
		attackCommand[attackCommandAmt] = val;
		//move to location
		switch(attackCommandAmt++) {
			case 0:
				MoveTo9Block(val);
				break;
			case 1:
				PrepareAttack();
				break;
			case 2:
				AttackBlock(attackCommand[1], attackCommand[2]);
				attackCommandAmt = 0;
				break;
			default:
				Debug.Log("ERROR: attackCommandAmt overflow " + val);
				break;
		}

	}


	void MoveTo9Block(int val) {
		playerSR.sprite = sprites[2];
		GameObject target9Block = GameObject.Find("9 Block " + val.ToString());
		playerAudio.clip = phase1Audio[Random.Range(0, 2)];
		playerAudio.Play();
		if (target9Block)
			gameObject.transform.position = target9Block.transform.position + new Vector3(0, 12, 0);
			
		else
			Debug.Log("CANNOT FIND");

		curr9Block = target9Block;
	}


	void PrepareAttack() {
		gameObject.transform.position += new Vector3(0, -4, 0);
		//choose random attack set
		int randomAttack = Random.Range(1, 4);
		currAttackSet = randomAttack;
		playerSR.sprite = sprites[currAttackSet];
		playerAudio.clip = phase2Audio[Random.Range(0, 2)];
		playerAudio.Play();


	}

	void AttackBlock(int row, int col) {
        Transform target;

		NineBlock nb = curr9Block.GetComponent<NineBlock>();
        target = nb.GetChildBlockTransform(row, col);
		gameObject.transform.position = target.position + new Vector3(-2.0f, 2.2f, playerDepth);

        Block block = target.gameObject.GetComponent<Block>();
        if(block.enemy != null) {
            playerAnim.SetTrigger("Attack" + (currAttackSet - 1).ToString());
            playerAudio.clip = phase3Audio[Random.Range(0, 2)];
            playerAudio.Play();
            block.GetAttacked(damageDealt);
        } else if(block.hazard != null) {
            //get hurt
        } else {
            playerAudio.clip = phase2Audio[Random.Range(0, 2)]; //went to a space without an enemy
            playerAudio.Play();
        }
	
	}
}
