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
	public AudioClip[] hurtAudio;

    public int damageDealt;
	public bool paralyzed;
	public bool canTakeInput;
	public float paralyzeDuration;
	// Use this for initialization
	void Awake() {
		canTakeInput = true;
	}
	void Start () {
		playerAnim = gameObject.GetComponent<Animator>();
		playerSR = gameObject.GetComponent<SpriteRenderer>();
		gameObject.GetComponent<SpriteRenderer>().sprite = testSprite;

		playerAudio = gameObject.GetComponent<AudioSource>();
		attackCommand = new int[attackCommandLen];
		playerDepth = gameObject.transform.position.z;

		paralyzed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("New State") && !paralyzed && canTakeInput) { //||
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
		//send message to display on HUD
		if(attackCommandAmt == 0)
			SendMessageUpwards("ReceivePlayerInput", -1); //negative value clears text
		SendMessageUpwards("ReceivePlayerInput", val);

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
		gameObject.transform.position = target9Block.transform.position + new Vector3(0, 12, 0);

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
        if(block.enemyObj != null && block.enemyObj.GetComponent<Enemy>().alive) {
			PerformAttack();
			Enemy e = block.enemyObj.GetComponent<Enemy>();
			e.TakeDamage(damageDealt);
        } else if(block.hazardObj != null) {
			playerAudio.clip = hurtAudio[Random.Range(0, hurtAudio.Length)];
			playerAudio.Play();
			StartCoroutine(ParalyzeTime(paralyzeDuration));
        } else if(block.itemObj != null) {
			Debug.Log("picking up plusTime");
			PlusTimeItem psi = block.itemObj.GetComponent<PlusTimeItem>();
			psi.PickUp();
		} else {
			LandOnEmpty();
        }
	
	}

	void PerformAttack() {
		//Time.timeScale = .1f;
		playerAnim.SetTrigger("Attack" + (currAttackSet - 1).ToString());
		playerAudio.clip = phase3Audio[Random.Range(0, 2)];
		playerAudio.Play();
	}

	void LandOnEmpty() {
		playerAudio.clip = phase2Audio[Random.Range(0, 2)]; //went to a space without an enemy
		playerAudio.Play();
	}

	public IEnumerator ParalyzeTime(float dur) {
		Color originalColor = playerSR.color;
		playerSR.color = new Color(.1f, .1f, 1f, .9f);
		paralyzed = true;
		yield return new WaitForSeconds(dur);
		playerSR.color = originalColor;
		paralyzed = false;
	}
}
