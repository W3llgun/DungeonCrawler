using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour {

    Rigidbody2D rigid;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;

    [HideInInspector]
    public int damage;

    public float speed;
    public float maxSpeed;
    
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        rigid.AddForce(direction * speed);
        rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, maxSpeed);

        Vector3 posForCam = Camera.main.ScreenToViewportPoint(transform.position);
        
        if (posForCam.x > 0.3 || posForCam.x < -0.3 || posForCam.y > 0.3 || posForCam.y < -0.3)
        {
            Delete();
        }
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Delete();
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            Delete();
        }
    }
}
