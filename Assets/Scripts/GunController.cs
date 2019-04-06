using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

    public float angle = 90;
    Transform center;
    public bool coRunning;
    public float deltaRotation;
    public bool spinning;
    public float time = 0.5f;
   public GameObject bullet;
    public float speed = 5;
    public Transform GunEnd, GunEnd1, GunEnd2;
    public float force = 6000;
    public float GunShotsMax = 5;
    public float currentGunShotCount;
    public Image gunshots;
    public Material gunShotColor;
    SimpleHealthBar bar;
    Updater updater;
    int oldLevel;
    void Start()
    {
        updater = GameObject.FindGameObjectWithTag("Updater").GetComponent<Updater>();
        currentGunShotCount = GunShotsMax;
        center = GameObject.FindGameObjectWithTag("Center").transform;
        gunshots.color = gunShotColor.color;
        bar = gunshots.GetComponent<SimpleHealthBar>();
        oldLevel = updater.level;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !spinning)
        {
            StartCoroutine(SpinMe(center, -angle, time));
        }
        if (Input.GetKeyDown(KeyCode.D) && !spinning)
        {
            StartCoroutine(SpinMe(center, angle, time));
        }
        if (Input.GetKeyDown(KeyCode.Space)&& !spinning&&currentGunShotCount>0){
            Shoot(GunEnd.gameObject);
            Shoot(GunEnd1.gameObject);
            Shoot(GunEnd2.gameObject);
            currentGunShotCount -= 1;
            bar.UpdateBar(currentGunShotCount, GunShotsMax);
           
        }
        if (updater.reset)
            {
                resetGunshot();
            }
    }
    public void reduceGunShot()
    {
        if (GunShotsMax == 1) return;
        GunShotsMax -= 1;
    }
    private void Shoot(GameObject which)
    {
        Vector3 curentGunPosition = GetPosition();
       GameObject temp= Instantiate(bullet, which.transform.position, which.transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(curentGunPosition*force);
    }
    public void resetGunshot()
    {   
        currentGunShotCount = GunShotsMax;
        bar.UpdateBar(currentGunShotCount, GunShotsMax);
    }

    private Vector3 GetPosition()
    {
        Vector3 temp = new Vector3();
        int val = int.Parse(Mathf.RoundToInt(GunEnd.transform.eulerAngles.y).ToString());
     //   Debug.Log("Gun Position: "+ val);
        if ( val<= 0)
        {
            temp = -Vector3.right;
            Debug.Log("If");
            return temp;
        }
        switch (val)
        {
            case 90:
                temp = Vector3.forward;
                Debug.Log("foward");
                break;
            case 180:
                temp = Vector3.right;
                Debug.Log("right switch");
                break;
            case 270:
                temp = -Vector3.forward;
                Debug.Log("-foward");
                break;
            case 360:
                temp = -Vector3.right;
                Debug.Log("-right siwtch");
                break;
        }
        return temp;
    }
    IEnumerator SpinMe(Transform spinAxis, float degrees, float inTime)
    {
        coRunning = true;
        Vector3 firstAxisPos = spinAxis.position;
        Vector3 firstPos = transform.position;
        Quaternion firstRot = transform.rotation;
        Vector3 firstRotVector = transform.eulerAngles;
        float radius = Vector3.Distance(spinAxis.position, transform.position);
        float rads = degrees * Mathf.Deg2Rad;
        float yPos = radius * Mathf.Sin(rads);
        float xPos = radius * Mathf.Cos(rads);
        Vector3 newPos = new Vector3(xPos, yPos, firstPos.z);
        Vector3 byAngles = new Vector3(0.0f, degrees, 0f);
        Quaternion newRot = Quaternion.Euler(transform.eulerAngles + byAngles);
        spinning = true;
        float perc = 0.0f;
        for (float i = 0.0f; i < inTime; i += Time.smoothDeltaTime)
        {
            if ((i + Time.smoothDeltaTime) >= inTime) i = inTime;
            else
            {
                perc = i / inTime;
                perc = perc * perc * (3f - 2f * perc);
            }
            transform.position = Vector3.Lerp(firstPos, newPos, perc);
            transform.rotation = Quaternion.Lerp(firstRot, newRot, perc);
            transform.position = Recalibrate(firstAxisPos, spinAxis.position);
            yield return null;
        }
        Vector3 degreeChange = Vector3.up * degrees;
        transform.Rotate((firstRotVector + degreeChange) - transform.eulerAngles);
        transform.position = Recalibrate(firstAxisPos, spinAxis.position);
        spinning = false;
        deltaRotation += degrees;
    }

    Vector3 Recalibrate(Vector3 firstPos, Vector3 newPos)
    {
        float xMove = firstPos.x - newPos.x;
        float yMove = firstPos.y - newPos.y;
        float zMove = firstPos.z - newPos.z;
        return new Vector3(transform.position.x + xMove, transform.position.y + yMove, transform.position.z + zMove);
    }
}
