using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour {

    Rigidbody2D rigid;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;

    public float speed;
    public float maxSpeed;

    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
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
            Delete();
        }
        else if (collision.gameObject.CompareTag("Borders"))
        {
            Delete();
        }
    }
}
