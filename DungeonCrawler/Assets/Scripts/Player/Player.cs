using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

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

    [SerializeField]
    List<Sprite> lifeBars;

    Text scoreText;
    Image lifeImage;

    Rigidbody2D rigid;
    BoxCollider2D coll;
    int life;
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }
    public int maxLife = 3;

    int score;

    public int maxSpeed = 15;
    public int speed = 5;

    public int damage = 1;

    public float fireRate = 0.5f;

    public Controls move = new Controls(KeyCode.Z, KeyCode.Q, KeyCode.S, KeyCode.D);
    public Controls shoot = new Controls(KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow);

    public GameObject bullet;
    private Coroutine shootCor;

    void Start ()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
		lifeImage = GameObject.Find("Life").GetComponent<Image>();
		coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        Life = maxLife;
		Debug.Log(888);
	}

	private void OnLevelWasLoaded(int level)
	{
		scoreText = GameObject.Find("Score").GetComponent<Text>();
		scoreText.text = "Score : " + score;

		lifeImage = GameObject.Find("Life").GetComponent<Image>();
		lifeImage.sprite = lifeBars[life];
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
        go.GetComponent<Bullet>().damage = damage;

        yield return new WaitForSeconds(fireRate);

        shootCor = null;
    }

    public void Hit(int damage)
    {
		if(lifeImage == null) lifeImage = GameObject.Find("Life").GetComponent<Image>();
		Life = Life - damage;
		lifeImage.sprite = lifeBars[life];
		if (Life <= 0)
		{
			MenuManager.instance.setEnd("Game Over");
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Pickup collect = collision.gameObject.GetComponent<Item>().Collect();
            if (collect.type == ItemType.LIFE)
            {
                Life = Life + (collect.value/2);
				Life = Mathf.Min(Life, maxLife);
                lifeImage.sprite = lifeBars[life];
            }
            else if(collect.type == ItemType.SCORE)
            {
				if (scoreText == null) scoreText = GameObject.Find("Score").GetComponent<Text>();
				score += collect.value;
                scoreText.text = "Score : " + score;
            }
        }
    }
}
