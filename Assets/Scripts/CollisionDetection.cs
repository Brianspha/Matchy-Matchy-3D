using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour {

    // Use this for initialization
    public GameObject splater;
    public float magn = 1000, rough = 500, fadeIn = 1f, fadeOut = 2f;
    public Image currentHelth;
    int score = 0;
     public float wallLife;
    public float maxWallLife=5;
    CameraShaker cameraShaker;
    RipplePostProcessor ripller;
    public float delay = 5f;
    Updater update;
    public float lifeDecrementor = .25f;
    SimpleHealthBar bar;
    public GameObject puzzleBrick;
    public AudioSource collisionClip;
    public AudioSource collisionClipWrong;

    public void Awake()
    {

    }
    public void Start()
    {
        wallLife = maxWallLife;
        cameraShaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShaker>();
        ripller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RipplePostProcessor>();
        update = GameObject.FindGameObjectWithTag("Updater").GetComponent<Updater>();
        bar = currentHelth.GetComponent<SimpleHealthBar>();
    }
    private void Update()
    {
        if(!update.GameOver()&&update.reset)
        {
            RestWallLife();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (update.GameOver()) return;
        collisionClipWrong.mute = false;
        collisionClip.mute = false;
        collisionClip.volume = 1;
        collisionClipWrong.volume = 1;
        if (other.gameObject.CompareTag("Puzzle"))
        {
           Material puzzleMaterial= other.gameObject.GetComponent<Renderer>().material;
            Material myMaterial = gameObject.GetComponent<Renderer>().material;
            PuzzleMover mover=other.gameObject.GetComponent<PuzzleMover>();
            if(puzzleMaterial.color != myMaterial.color)
            {
                wallLife -= 1;
               
                collisionClipWrong.Play();
                Debug.Log("WallLife: " + wallLife);
                bar.UpdateBar(wallLife, maxWallLife);
                cameraShaker.ShakeOnce(magn, rough, fadeIn,fadeOut);
                if(wallLife<=0)
                {
                    DestroyWall();
                }
                
            }
            if(puzzleMaterial.color == myMaterial.color)
            {
                collisionClip.Play();
                update.UpdateScore(1);
                ripller.Ripple();
                Debug.Log("Score: " + score);
                collisionClip.volume = 1;

            }
            Destroy(other.gameObject);
        }
        //collisionClipWrong.mute = true;
        //collisionClip.mute = true;
    }
    public void DestroyWall()
    {
        Destroy(gameObject);
    }
    public void RestWallLife()
    {
        wallLife = maxWallLife;
    }
}
