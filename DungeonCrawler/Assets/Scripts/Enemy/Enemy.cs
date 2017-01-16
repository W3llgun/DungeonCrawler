using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Moveable {
	protected Transform target;
	protected Vector3 targetDir;

	[Header("Stats")]
	public int life = 1;
	public int damage = 1;
	

	protected virtual void Start () {
		
	}
	
	protected virtual void Update()
	{
		if (target)
		targetDir = checkDirection(target);
	}

	protected Vector3 checkDirection(Transform targ)
	{
		return targ.position - transform.position;
	}

	public void takeDamage(int dmg)
	{
		life -= dmg;
		if(life <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	protected abstract void attack(Vector3 dir);
}
