using UnityEngine;

namespace Assets.Scripts {
	public class CameraControl : MonoBehaviour {

		public Ball ball;

		private Vector3 _offset;

		// Use this for initialization
		void Start() {
			_offset = transform.position - ball.transform.position;
		}

		// Update is called once per frame
		void Update() {

			if (ball.transform.position.z < 1700) {
				transform.position = ball.transform.position + _offset;
			}



		}
	}
}
