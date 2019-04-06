using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    // Use this for initialization
    public GameObject[] Puzzles;
    public float maxDelay = 3;
    public float currentDelay;
    public string whichWall;
    public bool myTurntoSpawn = false;
    SpawnerController mainController;
    public bool ToporBottom = false;
    GameObject current;
    public float angle = 90;
    Updater update;
	void Start () {
        currentDelay = maxDelay;
        update = GameObject.FindGameObjectWithTag("Updater").GetComponent<Updater>();
        mainController = GameObject.FindGameObjectWithTag("SpawnerController").GetComponent<SpawnerController>(); 
      //  Spawn();
    }

  

    // Update is called once per frame
    void Update () {
       
        if (!update.GameOver()&&myTurntoSpawn)
        {
          currentDelay -= Time.deltaTime;
            if(currentDelay<=0)
            {
                ChangeTurn(false);
                mainController.Spawn();
                currentDelay = maxDelay;
            }
        }
	}
    public void Spawn()
    {
        GameObject temp = Puzzles[UnityEngine.Random.Range(0, Puzzles.Length)];
        current = temp;
        current.SetActive(true);
        Vector3 modified = new Vector3(transform.position.x, temp.transform.position.y, transform.position.z);
        if (ToporBottom)
        {
            temp.transform.rotation =  Quaternion.Euler(temp.transform.rotation.x, angle, temp.transform.rotation.z);
            Instantiate(temp, modified, Quaternion.Euler(temp.transform.rotation.x, angle, temp.transform.rotation.z));
        }
        else
        {
            Instantiate(temp, modified, Quaternion.identity);
        }
    }
    public void DeleteSpawnedPuzzle()
    {
        // current = new GameObject();
        current.SetActive(false);
        currentDelay = maxDelay;
    }
    public void ChangeTurn(bool to)
    {
        myTurntoSpawn = to;
    }
}
