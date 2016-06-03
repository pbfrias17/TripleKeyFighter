using UnityEngine;
using System.Collections;

//Class that handles spawning of spawnables at random spots
public class SpawnController : MonoBehaviour {

    public int enemiesToSpawn;
	public int hazardsToSpawn;
    public GameObject[] spawnableEnemies;
    public GameObject[] spawnableHazards;
	public GameObject[] spawnableItems;
	public NineBlock[] nineBlocks;
	public enum spawnables { Enemies, Hazards, Items }

	//Configure what/amount to spawn
	public void Build(int enemies, int hazards) {
		enemiesToSpawn = enemies;
		hazardsToSpawn = hazards;
	}


	public void SpawnAll() {
		//enemy spawn is priority
		SpawnSpawnables((int)spawnables.Enemies, enemiesToSpawn);
		//hazards and items spawn are dependent
		SpawnSpawnables((int)spawnables.Hazards, hazardsToSpawn);
	}

	public void SpawnItem(int amountToSpawn) {
		SpawnSpawnables((int)spawnables.Items, amountToSpawn);
	}

	void SpawnSpawnables(int type, int amt) {
		int[] randomCoords = new int[3];
		int amtSpawned = 0;
		while (amtSpawned < amt) {
			RandomizeCoords(randomCoords);
			GameObject go = nineBlocks[randomCoords[0]].GetComponent<NineBlock>().GetChildBlockTransform(randomCoords[1], randomCoords[2]).gameObject;
			Block block = go.GetComponent<Block>();
			if (block.isEmpty) {
				block.isEmpty = false;
				amtSpawned++;
				switch (type) {
					//ew, fix this eventually
					case (int) spawnables.Enemies:
						GameObject enemyObj = Instantiate(spawnableEnemies[0], block.transform.position, Quaternion.identity) as GameObject;
						block.enemyObj = enemyObj;
						enemyObj.transform.parent = gameObject.transform;
						break;
					case (int) spawnables.Hazards:
						GameObject hazardObj = Instantiate(spawnableHazards[0], block.transform.position, Quaternion.identity) as GameObject;
						block.hazardObj = hazardObj;
						hazardObj.transform.parent = gameObject.transform;
						break;
					case (int) spawnables.Items:
						GameObject itemObj = Instantiate(spawnableItems[0], block.transform.position, Quaternion.identity) as GameObject;
						block.itemObj = itemObj;
						itemObj.transform.parent = gameObject.transform;
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
