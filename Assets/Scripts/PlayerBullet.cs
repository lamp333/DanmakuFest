using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    float speed;

	// Use this for initialization
	void Start () {
        speed = 40f;	
	}
	
	// Update is called moveToPoint per frame
	void Update () {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the player with an enemy ship or enemy bullet
        if (collision.tag == "EnemyShipTag" || collision.tag == "CirnoTag")
        {
            Destroy(gameObject);
        }
    }
}
