using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using NUnit.Framework;

namespace Assets.Editor {
	[TestFixture]
	public class ActionMasterTest
	{

		private List<int> _pinFalls;
		private readonly ActionMaster.Action _endTurn = ActionMaster.Action.EndTurn;
		private readonly ActionMaster.Action _tidy = ActionMaster.Action.Tidy;
		private readonly ActionMaster.Action _reset = ActionMaster.Action.Reset;
		private readonly ActionMaster.Action _endGame = ActionMaster.Action.EndGame;


		[SetUp]
		public void Setup()
		{
			_pinFalls = new List<int>();
		}

		[Test]
		public void T00PassingTest()
		{
			Assert.AreEqual(1, 1);
		}

		[Test]
		public void T01OneStrikeReturnsEndTurn()
		{
			_pinFalls.Add(10);
			Assert.AreEqual(_endTurn, ActionMaster.NextAction(_pinFalls));
		}

		[Test]
		public void T02Bowl8ReturnsTidy()
		{
			_pinFalls.Add(8);
			Assert.AreEqual(_tidy, ActionMaster.NextAction(_pinFalls));
		}

		[Test]
		public void T03Bowl2n8ReturnsEndTurn()
		{
			_pinFalls.Add(8);
			_pinFalls.Add(2);
			Assert.AreEqual(_endTurn, ActionMaster.NextAction(_pinFalls));
		}

		[Test]
		public void T04CheckResetAtStrikeInLastFrame()
		{
			int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
			Assert.AreEqual(_reset, ActionMaster.NextAction(rolls.ToList()));
		}

		[Test]
		public void T05CheckResetAtStrikeInLastFrame()
		{
			int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 };
			Assert.AreEqual(_reset, ActionMaster.NextAction(rolls.ToList()));
		}

		[Test]
		public void T06YouTubeRollsEndInEndGame()
		{
			int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2, 9 };
			Assert.AreEqual(_endGame, ActionMaster.NextAction(rolls.ToList()));
		}

		[Test]
		public void T07GameEndsAtBow20()
		{
			int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
			Assert.AreEqual(_endGame, ActionMaster.NextAction(rolls.ToList()));
		}

		[Test]
		public void T08StrikeOn19thFrame()
		{
			int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 5 };
			Assert.AreEqual(_tidy, ActionMaster.NextAction(rolls.ToList()));

		}

		[Test]
		public void T09MissedOn19thFrame()
		{
			int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0 };
			Assert.AreEqual(_tidy, ActionMaster.NextAction(rolls.ToList()));
		}

		[Test]
		public void T10MissedFirstFrameAndStrike()
		{
			int[] rolls = { 0, 10, 5, 1 };
			Assert.AreEqual(_endTurn, ActionMaster.NextAction(rolls.ToList()));
		}

		[Test]
		public void T11TripleStrikeOnLastFrames()
		{
			int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 };
			Assert.AreEqual(_endGame, ActionMaster.NextAction(rolls.ToList()));
		}

		[Test]
		public void T13ZeroGivesEndTurn()
		{
			int[] rolls = { 0, 1 };
			Assert.AreEqual(_endTurn, ActionMaster.NextAction(rolls.ToList()));
		}

	}
}
