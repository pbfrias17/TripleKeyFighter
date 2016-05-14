using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {


    public int enemiesToSpawn;
    public Enemy[] spawnableEnemies;
    public Hazard[] spawnableHazards;
    public NineBlock[] nineBlocks;


	// Use this for initialization
	void Start () {
        SpawnEnemies(enemiesToSpawn);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnEnemies(int amt) {
        int[] coords = new int[3];
        int spawnedEnemies = 0;
        while(spawnedEnemies != amt) {
            for (int i = 0; i < 3; i++)
            {
                coords[i] = Random.Range(0, 3);
            }
            NineBlock nb = nineBlocks[coords[0]].GetComponent<NineBlock>();
            GameObject go = nb.GetChildBlockTransform(coords[1], coords[2]).gameObject;
            Block block = go.GetComponent<Block>();
            if(block.isEmpty) {
                block.SpawnEnemy(spawnableEnemies[0]);
                spawnedEnemies++;
            }
        }
    }
}
