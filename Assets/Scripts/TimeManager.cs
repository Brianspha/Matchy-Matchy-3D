using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    // Use this for initialization
    public float slowDownFactor = .05f;
    public float slowDownLength = 2f;
    public float FixedDeltafactor = .099f;
    bool slowedTime = false;
	// Update is called once per frame
	void Update () {
        Time.timeScale += (1f / slowDownFactor) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}
    public void SlowDownTime()
    {
        Time.timeScale = Time.deltaTime * slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * FixedDeltafactor;

    }
}
