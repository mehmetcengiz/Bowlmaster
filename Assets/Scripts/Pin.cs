﻿using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    print(name + " " +IsStanding());
	}

    public bool IsStanding() {
        Vector3 rotationInEuler =  transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        return (tiltInX < standingThreshold && tiltInZ < standingThreshold);
    }
}
