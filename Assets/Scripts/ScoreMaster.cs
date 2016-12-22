using System.Collections.Generic;

namespace Assets.Scripts {
	public class ScoreMaster {

		//Return a list of cumulative scores, like a normal score card.
		public static List<int> ScoreCumulative(List<int> rolls) {
			List<int> cumulativeScores = new List<int>();
			int runningTotal = 0;


			foreach (int scoreFrame in ScoreFrames(rolls)) {
				runningTotal += scoreFrame;
				cumulativeScores.Add(runningTotal);
			}

			return cumulativeScores;
		}

		//Return a list of individual frame scores.
		public static List<int> ScoreFrames(List<int> rolls) {
			List<int> frames = new List<int>();

			for (int i = 1; i < rolls.Count; i += 2) {
				if (frames.Count == 10) {
					break;
				}

				if (rolls[i - 1] + rolls[i] < 10) {
					frames.Add(rolls[i - 1] + rolls[i]); //Normal frame
				}

				if (rolls.Count - i <= 1) {
					break;
				} //Ensure at least 1 available.

				if (rolls[i - 1] == 10) {
					//Strike
					i--; //Strike frame has just one bowl.
					frames.Add(10 + rolls[i + 1] + rolls[i + 2]);
				}
				else if (rolls[i - 1] + rolls[i] == 10) {
					frames.Add(10 + rolls[i + 1]); //Calculate SPARE bonus.
				}


			}

			return frames;
		}

	}
}