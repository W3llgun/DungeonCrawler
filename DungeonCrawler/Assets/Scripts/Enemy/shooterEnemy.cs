using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterEnemy : Enemy
{
	[Header("Shooter")]
	public float shootRate = 0.4f;
	public GameObject projectile;
	protected float lastShoot = 0;

	protected override void Start()
	{
		base.Start();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		if (target == null) Debug.LogError("No player found !");
	}

	protected override void Update()
	{
		base.Update();
		lastShoot += Time.deltaTime;
		if (lastShoot > shootRate)
		{
			lastShoot = 0;
			attackTarget();
		}
	}

	protected void FixedUpdate()
	{
		gotToTarget();
	}

	protected void gotToTarget()
	{
		move(targetDir, Time.fixedDeltaTime);
	}

	void attackTarget()
	{
		attack(targetDir);
	}

	protected override void attack(Vector3 dir)
	{
		if (projectile)
		{
			GameObject go = (GameObject)Instantiate(projectile);
			go.transform.position = transform.position;

			go.GetComponent<Projectile>().init(dir, target.tag, damage);
		}
	}
}
