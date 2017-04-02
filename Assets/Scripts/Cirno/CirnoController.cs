using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirnoController : MonoBehaviour {

    public float idleTime;
    public float moveTime;
    public float hp;
    public GameObject ExplosionGO;

    float nextMovement;
    Vector2 min;
    Vector2 max;
    float xlength;
    float ylength;
    float accel;

    Vector2 initialPos;
    Vector2 destPos;

    bool moveToPoint;
    bool startDecel;
    bool timerOn;
    float speed;

    GameObject DirectionBody;
    GameObject IdleBody;
    GameObject Head;


    // Use this for initialization
    void Start () {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        xlength = max.x - min.x;
        ylength = max.y - min.y;
        moveToPoint = true;
        startDecel = true;
        timerOn = false;
        initialPos = transform.position;
        destPos = new Vector2(0, ylength*8/10 + min.y);
        speed = Vector2.Distance(initialPos, destPos) / moveTime;
        DirectionBody = GameObject.Find("CirnoDirectionBody");
        IdleBody = GameObject.Find("CirnoIdleBody");
        Head = GameObject.Find("CirnoHead");
        DirectionBody.SetActive(false);

    }
	
	// Update is called moveToPoint per frame
	void Update () {
        if (moveToPoint) {
            float remainingDist = Vector2.Distance(transform.position, destPos);
            float totalDist = Vector2.Distance(initialPos, destPos);
            if (remainingDist - 0.1f < totalDist / 2)
            {
                
                if (startDecel)
                {
                    accel= speed * speed / (2 * remainingDist);
                    startDecel = false;
                }
                speed -= accel * Time.deltaTime*60;
                if(speed < 0)
                {
                    moveToPoint = false;
                    startDecel = true;
                    initialPos = transform.position;
                    nextMovement = Time.time + idleTime;
                    DirectionBody.SetActive(false);
                    IdleBody.SetActive(true);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, destPos, speed);
            float dist = Vector2.Distance(destPos, transform.position);
            if (dist < 0.1f)
            {
                moveToPoint = false;
                startDecel = true;
                initialPos = transform.position;
                nextMovement = Time.time + Random.Range(idleTime, idleTime+2);
                DirectionBody.SetActive(false);
                IdleBody.SetActive(true);

            }

        }
        else if (moveToPoint == false && Time.time > nextMovement)
        {
            moveToPoint = true;
            float xMin = xlength * 1 / 10 + min.x;
            float xMax = xlength * 9 / 10 + min.x;
            xMin = Mathf.Max(xMin, transform.position.x - xlength / 3);
            xMax = Mathf.Min(xMax, transform.position.x + xlength / 3);
            destPos = new Vector2(Random.Range(xMin,xMax ), Random.Range(ylength * 9f / 10f + min.y, ylength * 7 / 10 + min.y));
            speed = Vector2.Distance(initialPos, destPos) / moveTime;
            if (destPos.x > initialPos.x)
            {
                Head.GetComponent<SpriteRenderer>().flipX = true;
                IdleBody.GetComponent<SpriteRenderer>().flipX = true;
                DirectionBody.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                Head.GetComponent<SpriteRenderer>().flipX = false;
                IdleBody.GetComponent<SpriteRenderer>().flipX = false;
                DirectionBody.GetComponent<SpriteRenderer>().flipX = false;
            }
            DirectionBody.SetActive(true);
            IdleBody.SetActive(false);
        }

        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the player with an enemy ship or enemy bullet
        if (collision.tag == "PlayerBulletTag")
        {
            PlayBulletCollision();
            hp--;
        }
        if (hp <= 0)
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }
    //need to make sprite for bullet collision
    void PlayBulletCollision()
    {

    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

}
