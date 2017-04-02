using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Vector2Extension
{
    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }
}

public class EnemyGun : MonoBehaviour {

    public GameObject EnemyBulletGO;
    private int numFired;
    Vector3 playerPos;
    Vector3 firePos;

    // Use this for initialization
    void Start () {
        numFired = 0;		
	}
	
	// Update is called moveToPoint per frame
	void Update () {
		
	}
    //straight 5 shots of 3
    public void s_5_3()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
       

        if (playerShip != null)
        {
            if (numFired == 0)
            {
                playerPos = playerShip.transform.position;
                firePos = transform.position;
            }
            GameObject bullet1 = (GameObject)Instantiate(EnemyBulletGO);
            GameObject bullet2 = (GameObject)Instantiate(EnemyBulletGO);
            GameObject bullet3 = (GameObject)Instantiate(EnemyBulletGO);

            bullet1.transform.position = firePos;
            bullet2.transform.position = firePos;
            bullet3.transform.position = firePos;

            Vector2 direction1 = playerPos - bullet1.transform.position;
            Vector2 direction2 = Vector2Extension.Rotate(direction1, 30);
            Vector2 direction3 = Vector2Extension.Rotate(direction1, -30);
            bullet1.GetComponent<EnemyBullet>().SetDirection(direction1);
            bullet2.GetComponent<EnemyBullet>().SetDirection(direction2);
            bullet3.GetComponent<EnemyBullet>().SetDirection(direction3);
        }
        if (numFired < 4)
        {
            Invoke("s_5_3", 0.3f);
            numFired++;
        }
        else
        {
            numFired = 0;
        }
    }

    public void h_5_3()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null)
        {
            GameObject bullet1 = (GameObject)Instantiate(EnemyBulletGO);
            GameObject bullet2 = (GameObject)Instantiate(EnemyBulletGO);
            GameObject bullet3 = (GameObject)Instantiate(EnemyBulletGO);

            bullet1.transform.position = transform.position;
            bullet2.transform.position = transform.position;
            bullet3.transform.position = transform.position;

            Vector2 direction1 = playerShip.transform.position - bullet1.transform.position;
            Vector2 direction2 = Vector2Extension.Rotate(direction1, 30);
            Vector2 direction3 = Vector2Extension.Rotate(direction1, -30);
            bullet1.GetComponent<EnemyBullet>().SetDirection(direction1);
            bullet2.GetComponent<EnemyBullet>().SetDirection(direction2);
            bullet3.GetComponent<EnemyBullet>().SetDirection(direction3);
        }
        if (numFired < 4)
        {
            Invoke("h_5_3", 0.3f);
            numFired++;
        }
        else
        {
            numFired = 0;
        }
    }
}
