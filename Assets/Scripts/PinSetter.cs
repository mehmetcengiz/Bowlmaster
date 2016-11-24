using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;

    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    standingDisplay.text = CountStanding().ToString();
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
