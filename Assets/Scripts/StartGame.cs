using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    // Use this for initialization
    public AudioSource sound;
    public AudioSource main;
    public void Awake()
    {
        sound.volume = 0;
    }
	void Start () {
        main.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGameNow()
    {
        sound.volume=1;
        main.volume = 0;
        sound.Play();
        SceneManager.LoadScene("Main1");
    }
    public void ExitGame()
    {
        sound.volume = 1;
        main.volume = 0;
        sound.Play();
        Application.Unload();
    }
}
