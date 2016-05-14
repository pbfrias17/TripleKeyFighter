using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int maxHP;
    private int currHP;
    Animator anim;

	// Use this for initialization
	void Start () { 
        anim = gameObject.GetComponent<Animator>();
        currHP = maxHP;
        Debug.Log(currHP);
    }

    public void TakeDamage(int dmg) {
        Debug.Log("curr - dmg : " + currHP + " - " + dmg);
        currHP -= dmg;
        if(currHP < 0) Die();
    }

    void Die() {
        if (!anim) Debug.Log("anim null");
        anim.SetTrigger("Death");
    }

    public virtual void Attack() {
        //normal enemies don't attack
    }
}
