﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float launchSpeed = 600f;

    private Rigidbody rigidbody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
	    rigidbody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();

        Launch();
	}

    private void Launch() {
        rigidbody.velocity = new Vector3(0, 0, launchSpeed);
        audioSource.Play();
    }

    // Update is called once per frame
	void Update () {
	}
}