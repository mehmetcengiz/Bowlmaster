using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class ActionMasterOld {

		public enum Action {
			Tidy,
			Reset,
			EndTurn,
			EndGame
		};

		private readonly int[] bowls = new int[21];
		private int _bowl = 1;

		public static Action NextAction(List<int> pinFalls) {
			ActionMasterOld actionMaster = new ActionMasterOld();
			Action currentAction = new Action();

			foreach (int pinFall in pinFalls) {
				currentAction = actionMaster.Bowl(pinFall);
			}

			return currentAction;
		}

		private Action Bowl(int pins) {

			if (pins < 0 || pins > 10) {
				throw new UnityException("Invalid pins.");
			}

			bowls[_bowl - 1] = pins;

			if (_bowl == 21) {
				return Action.EndGame;
			}

			//Hanld last-frame special cases.
			if (_bowl >= 19 && pins == 10) {
				_bowl++;
				return Action.Reset;
			}
			else if (_bowl == 20) {
				_bowl++;
				if (bowls[19 - 1] == 10 && bowls[20 - 1] != 10) {
					return Action.Tidy;
				}
				if (((bowls[19 - 1] + bowls[20 - 1])%10) == 0) {
					return Action.Reset;
				}
				return Bowl21Awarded() ? Action.Tidy : Action.EndGame;
			}
			else if (_bowl%2 != 0) {
				// Frist bowl of frame
				if (pins == 10) {
					_bowl += 2;
					return Action.EndTurn;
				}
				_bowl += 1;
				return Action.Tidy;
			}
			else if (_bowl%2 == 0) {
				//Second bowl of frame
				_bowl += 1;
				return Action.EndTurn;
			}

			throw new UnityException("Not sure what action to return! ");
		}

		private bool Bowl21Awarded() {
			return (bowls[19 - 1] + bowls[20 - 1] >= 10);
		}

	}
}
