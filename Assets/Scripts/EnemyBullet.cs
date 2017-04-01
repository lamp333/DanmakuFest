using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    float speed; // bullet speed
    float min_speed;
    Vector2 min;
    Vector2 max;
    Vector2 _direction; // direction of bullet
    bool isReady; // when direction is set or not

    void Awake()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        speed = 4f;
        min_speed = 2f;
        isReady = false;
        
    }
	// Use this for initialization
	void Start () {
		
	}
    //set bullet direction
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        isReady = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isReady)
        {
            //bullet current position
            Vector2 position = transform.position;
            //get bullet new position
            position += _direction * speed * Time.deltaTime;
            if (speed > min_speed)
            {
                speed = speed - 0.01f;
            }
            //update bullet position
            transform.position = position;

            if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
                (transform.position.y < min.y)|| (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShipTag")
        {
            Destroy(gameObject);
        }
    }
}
