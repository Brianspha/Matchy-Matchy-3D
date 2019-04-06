using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    public float speed = 15f;
    Rigidbody rb;
    CameraShaker main;
    public float magn = 1000, rough = 500, fadeIn = 1f, fadeOut = 2f;
    Updater update;
   public GameObject puzzleBrick;
    TimeManager timeManager;
    SelfDestruct selfDestruct;
    GameObject GunEnd;
    public float YPos;
    void Start () {
        rb = GetComponent<Rigidbody>();
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShaker>();
        update = GameObject.FindGameObjectWithTag("Updater").GetComponent<Updater>();
        timeManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent <TimeManager>();
        GunEnd = GameObject.FindGameObjectWithTag("GunParent");
    }

    // Update is called once per frame
    void Update () {
        if (update.GameOver())
        {
            DestroyBullet();
        }
        transform.position += Vector3.forward * speed*Time.deltaTime;
        transform.position = new Vector3(transform.position.x, YPos, transform.position.z);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puzzle"))
        {
            int count = puzzleBrick.transform.childCount;

            SpawnDestructibleBrick(other.gameObject);
            main.ShakeOnce(magn,rough,fadeIn,fadeOut);
            DestroyBullet();
            DestroyPuzzle(other.gameObject);
            update.UpdateScore(Random.Range(5, 10));
        }
    }
    private Quaternion GetRotation(GameObject GunEnd)
    {
        Quaternion temp = new Quaternion();
        int val = int.Parse(Mathf.RoundToInt(GunEnd.transform.eulerAngles.y).ToString());
        if (val <= 0)
        {
            temp = Quaternion.Euler(-Vector3.right);
            return temp;
        }
        switch (val)
        {
            case 90:
                temp = Quaternion.Euler(Vector3.forward);
                break;
            case 180:
                temp = Quaternion.Euler(Vector3.right);
                break;
            case 270:
                temp = Quaternion.Euler(-Vector3.forward);
                break;
            case 360:
                temp = Quaternion.Euler(-Vector3.right);
                break;
        }
        return temp;
    }
    public void DestroyPuzzle(GameObject other)
    {
        Destroy(other.gameObject);
    }
    public void SpawnDestructibleBrick(GameObject other)
    {
        Instantiate(puzzleBrick, transform.position, other.transform.rotation);

    }
    public Material GetMaterial()
    {
        Material material = gameObject.GetComponent<Renderer>().material;
        int val = int.Parse(Mathf.RoundToInt(transform.rotation.y).ToString());

        switch (val)
        {
            case 90:
                material = GameObject.FindGameObjectWithTag("WallTop").GetComponent<Renderer>().material;
                    break;
            case 180:
                material = GameObject.FindGameObjectWithTag("WallRight").GetComponent<Renderer>().material;
                break;
            case -90:
                material = GameObject.FindGameObjectWithTag("WallBottom").GetComponent<Renderer>().material;
                break;
            case -180:
                material = GameObject.FindGameObjectWithTag("WallLeft").GetComponent<Renderer>().material;
                break;
            default:
                material = GameObject.FindGameObjectWithTag("WallLeft").GetComponent<Renderer>().material;
                break;
        }
        return material;
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
