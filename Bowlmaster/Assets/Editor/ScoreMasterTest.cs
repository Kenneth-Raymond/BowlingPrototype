using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class ScoreMasterTest : MonoBehaviour {
    [TestFixture]
    public class ScoreTest
    {
        private List<int> bowl;
        private ActionMaster.Action tidy;
        private ActionMaster.Action endGame;
        private ActionMaster.Action endTurn;
        private ActionMaster.Action reset;

        [SetUp]
        public void Setup()
        {
            endTurn = ActionMaster.Action.EndTurn;
            bowl = new List<int>();
            tidy = ActionMaster.Action.Tidy;
            endGame = ActionMaster.Action.EndGame;
            reset = ActionMaster.Action.Reset;
        }

        [Test]
        public void T00PassingTest()
        {
            Assert.AreEqual(1, 1);
        }
        [Test]
        public void T01OneStrikeReturnsEndTurn()
        {
            bowl.Add(10);
            Assert.AreEqual(endTurn, ActionMaster.NextAction(bowl));
        }

        [Test]
        public void T02Bowl8ReturnsTidy()
        {
            bowl.Add(8);
            Assert.AreEqual(tidy, ActionMaster.NextAction(bowl));
        }
        [Test]
        public void T03SpareReturnsEndTurn()
        {
            int[] bowls = { 8, 2 };
            Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
        }
        [Test]
        public void T04EndOfLastFrameReturnsEndGame()
        {
            //               1    2    3    4    5    6    7    8    9     10
            int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 8, 1 };
            Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
        }
        [Test]
        public void T05Frame20StrikeReturnsReset()
        {
            //               1    2    3    4    5    6    7    8    9     10
            int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10 };
            Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
        }
        [Test]
        public void T07SampleGameReturnsEndGame()
        {
            for (int x = 0; x <= 20; x++)
            {
                bowl.Add(Random.Range(1, 10));
            }
            Assert.AreEqual(endGame, ActionMaster.NextAction(bowl));
        }
        [Test]
        public void T08NoBowl21ReturnsEndGame()
        {
            //               1    2    3    4    5    6    7    8    9    10
            int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
        }
        [Test]
        public void T09NotAStikeOnFrame20WithAFrame21ReturnsTidy()
        {
            //               1    2    3    4    5    6    7    8    9     10
            int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1 };
            Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
        }
        [Test]
        public void T10GutterBallOnFrame20ReturnsTidy()
        {
            //               1    2    3    4    5    6    7    8    9     10
            int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0 };
            Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
        }
        [Test]
        public void T11StrikeOnSecondBowlOfOneFrameReturnsEndTurn()
        {
            bowl.Add(0);
            bowl.Add(10);
            Assert.AreEqual(endTurn, ActionMaster.NextAction(bowl));
        }
        [Test]
        public void T12Dondi10thFrameTurkey()
        {
            int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 };
            Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
        }
    }
}
