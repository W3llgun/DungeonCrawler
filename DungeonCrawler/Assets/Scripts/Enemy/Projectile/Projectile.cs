using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Moveable
{
	Vector3 direction;
	string targetTag;
	int damage;
	float lifeTime = 3;

	public void init(Vector3 dir, string tagToHit, int dmg)
	{
		damage = dmg;
		targetTag = tagToHit;
		direction = dir;
		Destroy(this.gameObject, lifeTime);
	}

	private void FixedUpdate()
	{
		move(direction, Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag(targetTag))
		{
			Debug.Log("Damage Player");
			Destroy(this.gameObject);
		}
	}

}
