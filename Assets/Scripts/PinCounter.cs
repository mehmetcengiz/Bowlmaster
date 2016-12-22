using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
	public class PinCounter : MonoBehaviour {
		public Text standingDisplay;

		private GameManager _gameManager;
		private bool _ballOutOfPlay;
		private int _lastStandingCount = -1;
		private int _lastSettledCount = 10;
		private float _lastChangeTime;

		// Use this for initialization
		void Start() {
			_gameManager = GameObject.FindObjectOfType<GameManager>();
		}

		// Update is called once per frame
		void Update() {
			standingDisplay.text = CountStanding().ToString();

			if (_ballOutOfPlay) {
				UpdateStandingCountAndSettle();
				standingDisplay.color = Color.red;
			}
		}

		public void Reset() {
			_lastSettledCount = 10;
		}

		void OnTriggerExit(Collider collider) {
			if (collider.gameObject.name == "Ball") {
				_ballOutOfPlay = true;
			}
		}

		void UpdateStandingCountAndSettle() {
			// Update the lastStandingCount
			// Call PinsHaveSettled() when they have
			int currentStanding = CountStanding();

			if (currentStanding != _lastStandingCount) {
				_lastChangeTime = Time.time;
				_lastStandingCount = currentStanding;
				return;
			}

			float settleTime = 3f; // How long to wait to consider pins settled
			if ((Time.time - _lastChangeTime) > settleTime) {
				// If last change > 3s ago
				PinsHaveSettled();
			}

		}

		void PinsHaveSettled() {
			var standing = CountStanding();
			var pinFall = _lastSettledCount - standing;
			_lastSettledCount = standing;

			_gameManager.Bowl(pinFall);

			_lastStandingCount = -1; // Indicates pins have settled, and ball not back in box
			_ballOutOfPlay = false;
			standingDisplay.color = Color.green;
		}

		int CountStanding() {
			int standing = 0;

			foreach (var pin in GameObject.FindObjectsOfType<Pin>()) {
				if (pin.IsStanding()) {
					standing++;
				}
			}

			return standing;
		}
	}
}