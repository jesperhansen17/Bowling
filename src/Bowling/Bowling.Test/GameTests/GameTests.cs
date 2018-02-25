using Bowling.Core.Exceptions;
using Bowling.Game;
using Bowling.Game.Interfaces;
using Bowling.Game.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Test.GameTests
{
    [TestClass]
    public class GameTests
    {
        [TestCategory("Game")]
        [TestMethod]
        public void CalculateGameScoreShouldSumAllFrames()
        {
            // Arrange            
            var game = new BowlingGame();
            game.Frames = GetFakeFrames();

            // Act
            game.CalculateGameScore();

            // Assert
            Assert.AreEqual((6 + 19 + 9), game.GetTotalScore());
        }

        [TestCategory("Game")]
        [TestMethod]
        [ExpectedException(typeof(ScoreNotCalculatedException))]
        public void GetTotalScoreShouldThrowScoreNotCalculatedException()
        {
            // Arrange            
            var game = new BowlingGame();
            game.Frames = GetFakeFrames();

            // Assert
            Assert.AreEqual((6 + 19 + 9), game.GetTotalScore());
        }

        private List<IFrame> GetFakeFrames()
        {
            var frames = new List<IFrame>();

            var frameOne = new Frame
            {
                FirstScore = 3,
                SecondScore = 3
            };

            var frameTwo = new Frame
            {
                FirstScore = 10,
                SecondScore = 0
            };

            var lastFrame = new LastFrame
            {
                FirstScore = 3,
                SecondScore = 6,
                ThirdScore = 0
            };

            frames.Add(frameOne);
            frames.Add(frameTwo);
            frames.Add(lastFrame);

            return frames;
        }
    }
}
