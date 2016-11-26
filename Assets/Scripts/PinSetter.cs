using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

    public int lastStandingCount = -1;
    public Text standingDisplay;
    public GameObject pinSet;


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
	        UpdateStandingCountAndSettle();
	    }
	}

    public void RaisePins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()){
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()){
            pin.LowerIfStanding();
        }
    }

    public void RenewPins() {
        Instantiate(pinSet, new Vector3(0, 20, 1829), Quaternion.identity);
    }
    void OnTriggerEnter(Collider collider) {
        GameObject thingHit = collider.gameObject;

        //Ball enters play box.
        if (thingHit.GetComponent<Ball>()) {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    private void UpdateStandingCountAndSettle() {
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

    private void PinsHaveSettled() {
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

}
