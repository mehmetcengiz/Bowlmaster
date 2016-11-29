using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;
    public bool inPlay = false;

    private Vector3 ballStartPos;
    private Rigidbody rigidbody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
	    rigidbody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
	    rigidbody.useGravity = false;

	    ballStartPos = transform.position;
	}

    public void Launch( Vector3 velocity) {
        inPlay = true;
        rigidbody.useGravity = true;
        rigidbody.velocity = velocity;

        audioSource.Play();
    }

    public void Reset() {
        inPlay = false;
        transform.position = ballStartPos;
        transform.rotation = Quaternion.identity;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.useGravity = false;


    }

}
