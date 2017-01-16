using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {
	protected Rigidbody2D rigid;
	protected SpriteRenderer spriteRenderer;

	[Header("Movement")]
	public float acceleration = 1;
	public float maxSpeed = 5;
	public float deceleration = 0.5f;

	protected virtual void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();
	}
	
	protected virtual void move(Vector3 dir, float deltaT)
	{
		if (rigid && dir != Vector3.zero)
		{
			Vector3 vel = rigid.velocity;
			vel += dir.normalized * acceleration * deltaT;
			vel.z = 0;
			vel = Vector3.ClampMagnitude(vel, maxSpeed);
			rigid.velocity = vel;

			if (dir.x > 0)
				spriteRenderer.flipX = false;
			else
				spriteRenderer.flipX = true;
		}
		else
		{
			rigid.velocity *= deceleration;
		}
	}
}
