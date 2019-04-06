using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	// Use this for initialization
	public float angle=90;
	Transform center;
    public bool coRunning;
    public float deltaRotation;
    public bool spinning;
    public float time = 0.5f;

    void Start () {
		center=GameObject.FindGameObjectWithTag("Center").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow) && !spinning)
		{
            StartCoroutine(SpinMe(center, -angle, time));
		}
        if (Input.GetKeyDown(KeyCode.RightArrow) && !spinning)
        {
            StartCoroutine(SpinMe(center, angle, time));
        }
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
        return new Vector3(transform.position.x + xMove, transform.position.y + yMove, transform.position.z+zMove);
    }
}
