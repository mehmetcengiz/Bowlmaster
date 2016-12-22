using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class GameManager : MonoBehaviour {

		private readonly List<int> _bowls = new List<int>();
		private PinSetter _pinSetter;
		private Ball _ball;
		private ScoreDisplay _scoreDisplay;

		// Use this for initialization
		void Start() {
			_pinSetter = GameObject.FindObjectOfType<PinSetter>();
			_ball = GameObject.FindObjectOfType<Ball>();
			_scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
		}

		public void Bowl(int pinFall) {
			_bowls.Add(pinFall);
			_ball.Reset();
			
			_pinSetter.PerformAction(ActionMaster.NextAction(_bowls));

			_scoreDisplay.FillRollCard(_bowls);


		}
	}
}
