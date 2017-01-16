using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	public List<Door> doors;
	public List<Enemy> enemysInRoom;
	public bool isPlayerIn = false;

	void Awake () {
		enemysInRoom = new List<Enemy>();
		doors = new List<Door>(GetComponentsInChildren<Door>());
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("azdadazd");
		if (collision.CompareTag("Player"))
		{
			isPlayerIn = true;
			switchEnemysState(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isPlayerIn = false;
			switchEnemysState(false);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isPlayerIn) return;

		if(collision.CompareTag("Enemy"))
		{
			Enemy en = collision.gameObject.GetComponent<Enemy>();
			if(en && !enemysInRoom.Contains(en))
			{
				enemysInRoom.Add(en);
				en.gameObject.SetActive(false);
			}
		}
	}

	void switchEnemysState(bool value)
	{
		foreach (var en in enemysInRoom)
		{
			if(en)
			en.gameObject.SetActive(value);
		}
	}


	// Update is called once per frame
	void Update () {
		//if(isPlayerIn && )
	}
}
