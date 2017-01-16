using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] prefabsEnemy;
	public GameObject player;
	GameObject enemyHolder;
	public Transform left;
	public Transform right;
	public Transform top;
	public Transform bot;
	public float xMax, xMin, yMax, yMin;
	public int enemyCountMax = 5;
	
	void Awake () {
		if(GameObject.FindGameObjectWithTag("Player") == null)
		{
			Instantiate(player, Vector3.zero, Quaternion.identity);
		}
		xMax = right.position.x;
		xMin = left.position.x;
		yMax = top.position.y;
		yMin = bot.position.y;
		enemyHolder = GameObject.Find("EnemyHolder");
		if (enemyHolder == null) enemyHolder = new GameObject("EnemyHolder");

		if(Door.reloadCount != 0)
		spawnOnAwake();
	}
	
	void spawnOnAwake()
	{
		int currentEnemyCount = Random.Range(1,enemyCountMax);
		
		for (int i = 0; i < currentEnemyCount; i++)
		{
			
			Vector3 pos = new Vector3(Random.Range(xMin,xMax),Random.Range(yMin,yMax),0);
			spawnOne(pos);
		}
	}

	void spawnOne(Vector3 pos)
	{
		int indexToSpawn = Random.Range(0, prefabsEnemy.Length);
		GameObject go = (GameObject)Instantiate(prefabsEnemy[indexToSpawn], enemyHolder.transform);
		go.transform.position = pos;
	}
}
