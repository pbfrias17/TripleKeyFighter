using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	static public int amtAlive = 0;
    public int maxHP;
	public int currHP;
	public bool alive = true;
    Animator anim;
	public Vector3 spawnOffset;

	// Use this for initialization
	void Start () {
		amtAlive++; //keep track of alive enemies
		transform.position += spawnOffset; 
        anim = gameObject.GetComponent<Animator>();
        currHP = maxHP;
    }

    public void TakeDamage(int dmg) {
		currHP--;
		//wait a split second for player animation
		if (currHP <= 0)
			StartCoroutine(DieWithDelay(.1f));
	}

    void Die() {
        anim.SetTrigger("Death");
		alive = false;
		amtAlive--;
		SendMessageUpwards("ReceiveDeathMessage", amtAlive);
    }

    public virtual void Attack() {
        //normal enemies don't attack
    }

	public IEnumerator DieWithDelay(float dur) {
		yield return new WaitForSeconds(dur);
		Die();

	}

	public void OnDestroy() {
		//since amtAlive is static, must manually decrement it when living enemies are destroyed (starting a new game)
		if (alive) {
			amtAlive--;
			Debug.Log("living enemy destroyed");
		}
	}
}
