using UnityEngine;

namespace Assets.Scripts {
	public class PinSetter : MonoBehaviour {


		public GameObject pinSet;

		private Animator _animator;
		private PinCounter _pinCounter;

		// Use this for initialization
		void Start() {
			_animator = GetComponent<Animator>();
			_pinCounter = GameObject.FindObjectOfType<PinCounter>();
		}


		public void RaisePins() {
			foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
				pin.RaiseIfStanding();
			}
		}

		public void LowerPins() {
			foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
				pin.LowerIfStanding();
			}
		}

		public void RenewPins() {
			Instantiate(pinSet, new Vector3(0, 20, 1829), Quaternion.identity);
		}

		public void PerformAction(ActionMaster.Action action) {
			if (action == ActionMaster.Action.Tidy) {
				_animator.SetTrigger("tidyTrigger");
			}
			else if (action == ActionMaster.Action.EndTurn || action == ActionMaster.Action.Reset) {
				_animator.SetTrigger("resetTrigger");
				_pinCounter.Reset();
			}
			else if (action == ActionMaster.Action.EndGame) {
				throw new UnityException("Don't know how to handle end game yet.");
			}
		}
	}
}
