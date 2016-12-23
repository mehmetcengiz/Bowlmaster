using UnityEngine;

namespace Assets.Scripts {
	[RequireComponent(typeof(Ball))]
	public class BallDragLaunch : MonoBehaviour {

		private Vector3 _dragStart, _dragEnd;
		private float _startTime, _endTime;
		private Ball _ball;
		private float _maxEdgeofBall;
		// Use this for initialization
		void Start() {
			_ball = GetComponent<Ball>();
			_maxEdgeofBall = 50;
		}

		public void MoveStart(float amount) {

			bool isBallMoveable;

			if (amount > 0) {
				//If balls goes right
				isBallMoveable = !(_ball.transform.position.x >= _maxEdgeofBall);
			}
			else {
				//If balls goes left
				isBallMoveable = !(_ball.transform.position.x <= -_maxEdgeofBall);
			}

			if (!_ball.inPlay && isBallMoveable) {
				_ball.transform.Translate(new Vector3(amount, 0, 0));
			}
		}

		public void DragStar() {
			// Capture time & position of drag start.
			if (!_ball.inPlay) {
				_dragStart = Input.mousePosition;
				_startTime = Time.time;
			}
		}

		public void DragEnd() {

			if (!_ball.inPlay) {

				// Launch the ball.
				_dragEnd = Input.mousePosition;
				_endTime = Time.time;

				float dragDuration = _endTime - _startTime;

				float launchSpeedX = (_dragEnd.x - _dragStart.x)/dragDuration;
				float launchSpeedZ = (_dragEnd.y - _dragStart.y)/dragDuration;

				var launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);

				_ball.Launch(launchVelocity);
			}

		}
	}
}
