using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Enemy
{
	protected override void Start()
	{
		base.Start();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		if (target == null) Debug.LogError("No player found !");
	}

	protected override void Update()
	{
		base.Update();
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

	}

	protected override void attack(Vector3 dir)
	{
		//throw new NotImplementedException();
	}
}
