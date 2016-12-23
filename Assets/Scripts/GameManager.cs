using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class GameManager : MonoBehaviour {

		private readonly List<int> _rolls = new List<int>();
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
			try {
				_rolls.Add(pinFall);
				_ball.Reset();
				_pinSetter.PerformAction(ActionMaster.NextAction(_rolls));
			}
			catch {
				Debug.LogWarning("Something went wrong in Bowl");
			}

			try {
				_scoreDisplay.FillRolls(_rolls);
				_scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(_rolls));
			}
			catch {
				Debug.Log("FillRolls failed.");
			}


		}
	}
}
