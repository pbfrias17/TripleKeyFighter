using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public Enemy enemy;
    public Hazard hazard;
    public bool isEmpty;
    public bool playerLanded;
    public Vector3 spawnOffset;

	// Use this for initialization
	void Start () {
        isEmpty = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnEnemy(Enemy enemyToSpawn) {
        enemy = enemyToSpawn;
        Instantiate(enemy, transform.position + spawnOffset, Quaternion.identity);
        isEmpty = false;
    }

    public void GetAttacked(int dmg) {
        if(enemy != null) {
            //enemy.TakeDamage(dmg);
            Enemy e = enemy.GetComponent<Enemy>();
            e.TakeDamage(dmg);
        }
    }

}
