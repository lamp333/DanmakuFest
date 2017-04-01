using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO;
    GameObject hitBox;
    public float fireRate;
    private float lastFire = 0;



    public float speed;
    bool focus;
	// Use this for initialization
	void Start () {
        focus = false;
        hitBox = GameObject.Find("PlayerHitBox");
        hitBox.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //start in non focus mode first;
        focus = false;
        //fire bullet when z is pressed
        if (Input.GetButton("Focus"))
        {
            focus = true;            
        }
        hitBox.SetActive(focus);
        if (Input.GetButton("Fire1") && Time.time > lastFire)
        {
            lastFire = Time.time + fireRate;
            //instantiate first bullet
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;
            //instantiate second bullet
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }


		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Vector2 direction = new Vector2 (x,y).normalized;
		Move (direction);
	}

	void Move (Vector2 direction){
        //camera border
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        //account for character pixel length and width
		max.x = max.x - 0.225f;
		min.x = min.x + 0.225f;
		max.y = max.y - 0.285f;
		min.y = min.y + 0.285f;

		// character current position
		Vector2 pos = transform.position;
		//sets boundaries
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
        if (focus)
        {
            pos += direction * speed * Time.deltaTime *6/11;
        }
        else
        {
            pos += direction * speed * Time.deltaTime;
        }
		
		transform.position = pos;
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the player with an enemy ship or enemy bullet
        if ((collision.tag == "EnemyShipTag")|| (collision.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            //comment out for testing
            //Destroy(gameObject);
        }    
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

}
