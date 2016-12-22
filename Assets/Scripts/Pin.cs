using UnityEngine;

namespace Assets.Scripts {
	public class Pin : MonoBehaviour {

		public float standingThreshold = 3f;
		public float distToRaise = 40f;

		private Rigidbody _rigidBody;

		// Use this for initialization
		void Start() {
			_rigidBody = GetComponent<Rigidbody>();
		}

		// Update is called once per frame
		void Update() {
		}

		public void RaiseIfStanding() {
			if (IsStanding()) {
				_rigidBody.useGravity = false;
				transform.Translate(new Vector3(0, distToRaise, 0), Space.World);
				transform.rotation = Quaternion.Euler(270f, 0, 0);
			}
		}

		public void LowerIfStanding() {
			if (IsStanding()) {
				transform.Translate(new Vector3(0, -distToRaise, 0), Space.World);
				_rigidBody.useGravity = true;
			}
		}

		public void Renew() {
			Debug.Log("Renew Pins");
		}

		public bool IsStanding() {
			Vector3 rotationInEuler = transform.rotation.eulerAngles;

			float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
			float tiltInZ = Mathf.Abs(rotationInEuler.z);

			return (tiltInX < standingThreshold && tiltInZ < standingThreshold);
		}
	}
}
