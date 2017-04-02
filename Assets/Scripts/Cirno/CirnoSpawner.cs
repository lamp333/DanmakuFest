using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirnoSpawner : MonoBehaviour {
    public GameObject CirnoGO;
    // Use this for initialization
    void Start () {
        SpawnCirno();
	}
	
	// Update is called moveToPoint per frame
	void Update () {
		
	}
    void SpawnCirno()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject Cirno = (GameObject)Instantiate(CirnoGO);
        Cirno.transform.position = new Vector2(0, max.y);
    }
}
