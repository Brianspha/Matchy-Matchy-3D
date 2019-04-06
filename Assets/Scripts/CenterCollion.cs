using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CenterCollion : MonoBehaviour {

    // Use this for initialization
    GunController controller;
        public float magn = 100000, rough = 10000, fadeIn = 2f, fadeOut = 1.5f;
    CameraShaker cameraShaker;

    void Start () {
        controller = GameObject.FindGameObjectWithTag("GunParent").GetComponent<GunController>();
        cameraShaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShaker>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puzzle"))
        {
            controller.reduceGunShot();
            cameraShaker.ShakeOnce(magn, rough, fadeIn, fadeOut);

        }
    }
    }
