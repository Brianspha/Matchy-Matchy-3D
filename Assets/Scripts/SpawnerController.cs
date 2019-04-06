using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    // Use this for initialization
    public Spawner[] Spawners;
    Spawner current;
    public Updater update;
    public bool beenCalled = false;

	void Start () {
        update = GameObject.FindGameObjectWithTag("Updater").GetComponent<Updater>();
        Spawn();	
	}
	
	// Update is called once per frame
	void Update () {
        if(update.reset||update.GameOver())
        {
            current.DeleteSpawnedPuzzle();
        }
	}
    public void Spawn()
    {
        current = Spawners[Random.Range(0, Spawners.Length)];
        current.ChangeTurn(true);
        current.Spawn();
    }

}
