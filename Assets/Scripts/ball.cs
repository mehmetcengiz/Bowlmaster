using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;

    private Rigidbody rigidbody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
	    rigidbody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
	    rigidbody.useGravity = false;
        
	}

    public void Launch( Vector3 velocity){
        rigidbody.useGravity = true;
        rigidbody.velocity = velocity;

        audioSource.Play();
    }

}
