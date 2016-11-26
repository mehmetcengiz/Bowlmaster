using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Ball))]
public class BallDragLaunch : MonoBehaviour {

    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    private Ball ball;
    private float maxEdgeofBall;

	// Use this for initialization
	void Start () {
	    ball = GetComponent<Ball>();
	    maxEdgeofBall = 50;
	}

    public void MoveStart(float amount) {

        bool isBallMoveable;
        
        if (amount > 0) {
            //If balls goes right
            isBallMoveable = !(ball.transform.position.x >= maxEdgeofBall);
        }
        else {
            //If balls goes left
            isBallMoveable = !(ball.transform.position.x <= -maxEdgeofBall);
        }

        if (!ball.inPlay && isBallMoveable) {
           ball.transform.Translate(new Vector3(amount, 0, 0));
        }
    }

    public void DragStar() {
        // Capture time & position of drag start.
        dragStart = Input.mousePosition;
        startTime = Time.time;
    }

    public void DragEnd() {
        // Launch the ball.
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;

        float launchSpeedX = (dragEnd.x - dragStart.x)/dragDuration;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;
        
        Vector3 launchVelocity = new Vector3(launchSpeedX,0,launchSpeedZ);

        
        ball.Launch(launchVelocity);

    }
}
