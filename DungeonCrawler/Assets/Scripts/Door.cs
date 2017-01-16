using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
	public static int reloadCount = 0;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Player"))
		{
			if(reloadCount < 5)
			{
				Transform pTrsm = collision.collider.transform;
				if (pTrsm.position.x > 5 || pTrsm.position.x < -5)
				{
					pTrsm.position = new Vector3(pTrsm.position.x * -1, pTrsm.position.y, 0);
				}
				else
				{
					pTrsm.position = new Vector3(pTrsm.position.x, pTrsm.position.y * -1, 0);
				}
				reloadCount++;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			else
			{
				MenuManager.instance.setEnd("You Win");
			}
		}
	}
}
