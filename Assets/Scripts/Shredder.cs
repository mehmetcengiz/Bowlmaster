using UnityEngine;

namespace Assets.Scripts {
	public class Shredder : MonoBehaviour {

		//Shredes the pins when it left the collider.
		void OnTriggerExit(Collider collider) {
			GameObject thingLeft = collider.gameObject;

			if (thingLeft.GetComponent<Pin>()) {
				Destroy(thingLeft);
			}
		}
	}
}
