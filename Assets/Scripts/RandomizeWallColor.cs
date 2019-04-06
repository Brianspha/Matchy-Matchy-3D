using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeWallColor : MonoBehaviour {

    // Use this for initialization
    public Material[] Materials;
    public GameObject[] Walls;
    public Image[] healthBars;

    void Start () {
        Initialise();
       

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void Initialise()
    {
        Shuffle(Materials);
        for(int i=0; i < Walls.Length;i++)
        {
            Walls[i].gameObject.GetComponent<Renderer>().material = Materials[i];
            healthBars[i].color = Materials[i].color;
        }
    }
   public void Shuffle(Material[] list)
    {
        System.Random random = new System.Random();
        int n = list.Length;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            var temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
}
