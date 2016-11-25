using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

    public int lastStandingCount = -1;
    public Text standingDisplay;
    public float distanceToRaise = 40f;


    private Ball ball;
    private float lastChangeTime;
    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start () {
	    ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
	    standingDisplay.text = CountStanding().ToString();

	    if (ballEnteredBox) {
	        CheckStanding();
	    }
	}

    public void RaisePins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                Debug.Log("a pin standing.");
                pin.transform.Translate(new Vector3(0, distanceToRaise, 0));
            }
        }
    }

    public void LowerPins() {
        //lower standing pins only by distanceToRaise.
        Debug.Log("Lower Pins");
    }

    public void RenewPins() {
        Debug.Log("Renew Pins");
    }

    void CheckStanding() {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f; // waiting 3 second.
        if ((Time.time - lastChangeTime) > settleTime) { // when last change more than 3 ago.
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled() {
        ball.Reset();
        lastStandingCount = -1; // Indicates pins have settled, and ball not back in box.
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    public int CountStanding() {
        int countOfStandingPins =0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                countOfStandingPins++;
            }
        }
        return countOfStandingPins;
    }

    void OnTriggerExit(Collider collider) {
        GameObject thingLeft = collider.gameObject;

        if (thingLeft.GetComponent<Pin>()) {
            Destroy(thingLeft);
        }
    }

    void OnTriggerEnter(Collider collider) {
        GameObject thingHit = collider.gameObject;

        //Ball enters play box.
        if (thingHit.GetComponent<Ball>()) {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

}
