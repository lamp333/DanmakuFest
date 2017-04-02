using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public static int destroyed = 0;
    float xspeed= 0;
    float yspeed =2f;
    public float fireRate;
    public GameObject ExplosionGO;
    private float lastFire = 0;
    Vector2 min;
    Vector2 max;
    GameObject gun;

    float xlength;
    float ylength;
    Vector2 startPosition;

    int xdir;
    int ydir;
    float randomReturnRange;
    float randomCurveDirection;


    // Use this for initialization
    void Start () {
        checkDir();
        gun = gameObject.transform.GetChild(0).gameObject;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        xlength = max.x - min.x;
        ylength = max.y - min.y;
        fireRate = 3.5f;
        startPosition = transform.position;
        randomReturnRange = Random.Range(ylength * (1f / 4f) + min.y, ylength * (3f / 5f) + min.y);
        randomCurveDirection = Random.Range(0f, 1f);

    }
	
	// Update is called moveToPoint per frame
	void Update () {
        checkDir();
        Vector2 position = transform.position;
        CurveAndReturn(position, 0.1f, 0.01f, -8f, 2f);
        position = new Vector2(position.x - xspeed * Time.deltaTime, position.y - yspeed * Time.deltaTime);
        transform.position = position;

        if (Time.time > lastFire)
        {
            lastFire = Time.time + fireRate;
            gun.GetComponent<EnemyGun>().h_5_3();
        }
        

        if ((position.x < min.x) || (position.x > max.x) ||
            (position.y < min.y) || (position.y > max.y))
        {
            Destroy(gameObject);
            destroyed++;
            Debug.Log(destroyed);
        }
    }
    //
    void CurveAndReturn(Vector2 position, float yaccel, float xaccel, float end_yspeed, float mid_xspeed)
    {
        float xRandomDir;
        if (randomCurveDirection > 0.5f)
        {
            xRandomDir = 1;
        }
        else
        {
            xRandomDir = -1;
        }
        if (position.y < randomReturnRange)
        {
            if (yspeed > end_yspeed)
            {
                yspeed -= yaccel;                
            }
        }
        if (xspeed < mid_xspeed && ydir == -1)
        {
            xspeed += xaccel*xRandomDir;
        }
        else if (xspeed > 0 && ydir == 1)
        {
            xspeed -= xaccel * xRandomDir;
        }
    }    
    void checkDir()
    {
        if (yspeed > 0)
            ydir = -1;
        else if (yspeed < 0)
            ydir = 1;
        else
            ydir = 0;
        if (xspeed > 0)
            xdir = -1;
        else if (yspeed < 0)
            xdir = 1;
        else
            xdir = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the enemy with a player bullet
        if (collision.tag == "PlayerBulletTag")
        {
            PlayExplosion();
            Destroy(gameObject);
            destroyed++;
            Debug.Log(destroyed);
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
