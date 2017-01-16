using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	public static Room instance;
	public List<Door> doors;
	public List<Enemy> enemysInRoom;
	public bool isPlayerIn = false;
	bool doorOpen = false;

	void Awake () {
		instance = this;
		doors = new List<Door>(GetComponentsInChildren<Door>());
		foreach (var door in doors)
		{
			door.gameObject.SetActive(false);
		}

		InvokeRepeating("checkDoor", 0.2f, 1);
	}
	
	public void register(Enemy en)
	{
		if(!enemysInRoom.Contains(en))
		{
			enemysInRoom.Add(en);
		}
	}

	public void unregister(Enemy en)
	{
		if (enemysInRoom.Contains(en))
		{
			enemysInRoom.Remove(en);
			checkDoor();
		}
	}

	void checkDoor()
	{
		if (enemysInRoom.Count == 0 && !doorOpen)
		{
			openRandomDoor();
			//foreach (var door in doors)
			//{
			//	door.gameObject.SetActive(true);
			//}
		}
	}

	void openRandomDoor()
	{
		int openedCount = 0;
		foreach (var door in doors)
		{
			int rnd = Random.Range(1, 100);
			if(rnd > 50)
			{
				openedCount++;
				door.gameObject.SetActive(true);
			}
		}
		if(openedCount == 0)
		{
			doors[Random.Range(0, doors.Count)].gameObject.SetActive(true);
		}
		doorOpen = true;
	}
}
