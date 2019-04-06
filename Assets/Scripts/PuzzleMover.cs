using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMover : MonoBehaviour {

    // Use this for initialization
    public Transform center;
    public float Speed;
    public Material[] Materials;
    Vector3 modified;
    Updater update;
    Material myMaterial;
    SimpleHealthBar bar;
	void Start () {
        
        update = GameObject.FindGameObjectWithTag("Updater").GetComponent<Updater>();
        Speed = update.GetSpeed();
        gameObject.GetComponent<Renderer>().material = Materials[UnityEngine.Random.Range(0, Materials.Length)];
        center = GameObject.FindGameObjectWithTag("Center").transform;
        modified = new Vector3(center.transform.position.x, transform.position.y, center.transform.position.y);
        myMaterial = gameObject.GetComponent<Renderer>().material;

    }
	
	// Update is called once per frame
	void Update () {
        if (update.GameOver())
        {
            DestroyMe();
        }
        transform.position = Vector3.MoveTowards(transform.position, modified, Speed * Time.deltaTime);
	}

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    public Material GetWallMaterial()
    {
        return myMaterial;
    }
    public void updateSpeed()
    {
        Speed = update.Speed;
    }
}
