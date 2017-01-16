using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player : MonoBehaviour {

    [Serializable]
    public struct Controls
    {
        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;

        public Controls(KeyCode z, KeyCode q, KeyCode s, KeyCode d)
        {
            up = z;
            left = q;
            down = s;
            right = d;
        }
    }

    Rigidbody2D rigid;
    BoxCollider2D coll;
    int life;
    public int Life
    {
        get
        {
            return life / 4;
        }
        set
        {
            life = value * 4;
        }
    }
    public int maxLife = 3;
    public int maxSpeed = 15;
    public int speed = 5;

    public float fireRate = 0.5f;

    public Controls move = new Controls(KeyCode.Z, KeyCode.Q, KeyCode.S, KeyCode.D);
    public Controls shoot = new Controls(KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow);

    public GameObject bullet;
    private Coroutine shootCor;

    void Start ()
    {
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        Life = maxLife;
	}
	
	void FixedUpdate ()
    {
	    if (Input.GetKey(move.up))
	    {
            rigid.AddForce(Vector2.up * speed);
	    }
        else if (Input.GetKey(move.down))
        {
            rigid.AddForce(Vector2.down * speed);
        }

        if (Input.GetKey(move.right))
        {
            rigid.AddForce(Vector2.right * speed);
        }
        else if (Input.GetKey(move.left))
        {
            rigid.AddForce(Vector2.left * speed);
        }

        rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, maxSpeed);

        if (shootCor == null && (Input.GetKey(shoot.left) || Input.GetKey(shoot.up) || Input.GetKey(shoot.down) || Input.GetKey(shoot.right)))
        {
            shootCor = StartCoroutine( Shoot() );
        }
    }

    IEnumerator Shoot()
    {
        Vector3 vec = Vector3.zero;

        if (Input.GetKey(shoot.up))
        {
            vec = Vector3.up;
        }
        else if (Input.GetKey(shoot.down))
        {
            vec = Vector3.down;
        }
        else if (Input.GetKey(shoot.right))
        {
            vec = Vector3.right;
        }
        else if (Input.GetKey(shoot.left))
        {
            vec = Vector3.left;
        }

        GameObject go = Instantiate(bullet, transform.position + Vector3.Scale(vec, coll.bounds.extents), bullet.transform.rotation) as GameObject;
        go.GetComponent<Bullet>().direction = vec;

        yield return new WaitForSeconds(fireRate);

        shootCor = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Pickup collect = collision.gameObject.GetComponent<Item>().Collect();
            if (collect.type == "Life")
            {
                Life = Life + collect.value;
            }
            else if(collect.type == "Score")
            {
                 //AddScore
            }
        }
    }
}
