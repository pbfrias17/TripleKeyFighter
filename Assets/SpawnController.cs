using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

    public int enemiesToSpawn;
	public int hazardsToSpawn;
    public GameObject[] spawnableEnemies;
    public GameObject[] spawnableHazards;
    public NineBlock[] nineBlocks;
	public enum spawnables { Enemies, Hazards }


	public void Build(int enemies, int hazards) {
		enemiesToSpawn = enemies;
		hazardsToSpawn = hazards;
	}


	public void SpawnAll() {
		SpawnSpawnables((int)spawnables.Enemies, enemiesToSpawn);
		SpawnSpawnables((int)spawnables.Hazards, hazardsToSpawn);
	}


	void SpawnSpawnables(int type, int amt) {
		int[] randomCoords = new int[3];
		int amtSpawned = 0;
		GameObject enemyObj; //need a handle to atleast 1 enemyObj

		while (amtSpawned < amt) {
			RandomizeCoords(randomCoords);
			GameObject go = nineBlocks[randomCoords[0]].GetComponent<NineBlock>().GetChildBlockTransform(randomCoords[1], randomCoords[2]).gameObject;
			Block block = go.GetComponent<Block>();
			if(block.isEmpty) {
				block.isEmpty = false;
				amtSpawned++;
				switch (type) {
					case (int) spawnables.Enemies:
						enemyObj = Instantiate(spawnableEnemies[0], block.transform.position, Quaternion.identity) as GameObject;
						block.enemyObj = enemyObj;
						enemyObj.transform.parent = gameObject.transform;
						break;
					case (int) spawnables.Hazards:
						GameObject hazardObj = Instantiate(spawnableHazards[0], block.transform.position, Quaternion.identity) as GameObject;
						block.hazardObj = hazardObj;
						hazardObj.transform.parent = gameObject.transform;
						break;
				}
			}
		}
	}

	void RandomizeCoords(int[] coords) {
		for (int i = 0; i < coords.Length; i++) {
			coords[i] = Random.Range(0, 3);
		}
	}
}
