using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
    
    public float standingThreshold = 3f;
    public float distanceToRaise = 40f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void RaiseIfStanding() {
        if (IsStanding()) {
            rigidBody.useGravity = false;
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
        }
    }
    public void LowerIfStanding(){
        if (IsStanding()){
            transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
            rigidBody.useGravity = true;
        }
    }

    public void Renew(){
        Debug.Log("Renew Pins");
    }

    public bool IsStanding() {
        Vector3 rotationInEuler =  transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        return (tiltInX < standingThreshold && tiltInZ < standingThreshold);
    }
}
