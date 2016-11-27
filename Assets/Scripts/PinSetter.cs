using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

    public int lastStandingCount = -1;
    public Text standingDisplay;
    public GameObject pinSet;


    private Ball ball;
    private float lastChangeTime;
    private int lastSettledCount = 10;
    private bool ballEnteredBox = false;
    private ActionMaster actionMaster = new ActionMaster();
    private Animator animator;

	// Use this for initialization
	void Start () {
	    ball = GameObject.FindObjectOfType<Ball>();
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    standingDisplay.text = CountStanding().ToString();

	    if (ballEnteredBox) {
	        UpdateStandingCountAndSettle();
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
    public int CountStanding() {
        int countOfStandingPins =0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                countOfStandingPins++;
            }
        }
        return countOfStandingPins;
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
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;
        

        ActionMaster.Action action = actionMaster.Bowl(pinFall);
        Debug.Log(action);

        TakeAction(action);

        ball.Reset();
        lastStandingCount = -1; // Indicates pins have settled, and ball not back in box.
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    private void TakeAction(ActionMaster.Action action) {
        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.EndTurn || action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Don't know how to handle end game yet.");
        }
    }

}
