using Bowling.Game.Exceptions;
using Bowling.Game.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Test.FrameTest
{
    [TestClass]
    public class FrameTests
    {
        [TestCategory("Frame")]
        [TestMethod]
        public void CalculateOpenShouldSumFirstAndSecondScore()
        {
            // Arrange
            var firstScore = 4;
            var secondScore = 3;
            var frame = new Frame()
            {
                FirstScore = firstScore,
                SecondScore = secondScore
            };

            // Act
            var openFrameScore = frame.CalculateOpenFrame();

            // Assert
            Assert.AreEqual(7, openFrameScore);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void CalculatePreviousAsSpareScoreFirstFrame()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5
            };
            var spareFrame = new Frame();

            // Act 
            var spareScore = currentFrame.CalculatePreviousSpare(spareFrame);

            // Assert
            Assert.AreEqual((10 + 5), spareScore);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void CalculatePreviousFrameAsSpareScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5
            };

            var spareFrame = new Frame()
            {
                PreviousFrame = new Frame
                {
                    TotalScore = 8
                }
            };

            // Act 
            var spareScore = currentFrame.CalculatePreviousSpare(spareFrame);

            // Assert
            Assert.AreEqual((8 + 10 + 5), spareScore);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void CalculatePreviousFrameStrikeScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5,
                SecondScore = 3
            };

            var strikeFrame = new Frame()
            {
                PreviousFrame = new Frame
                {
                    TotalScore = 8
                }
            };

            // Act
            var strikeScore = currentFrame.CalculatePreviousStrike(strikeFrame);

            // Assert
            Assert.AreEqual((8 + 10 + 5 + 3), strikeScore);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void CalculatePreviousAsStrikeScoreFirstFrame()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5,
                SecondScore = 2
            };
            var strikeFrame = new Frame();

            // Act 
            var strikeScore = currentFrame.CalculatePreviousStrike(strikeFrame);

            // Assert
            Assert.AreEqual((10 + 5 + 2), strikeScore);
        }

        [TestCategory("Frame")]
        [TestMethod]
        [ExpectedException(typeof(InvalidScoreException), "InvalidScoreException not thrown")]
        public void CalculateScoreShouldThrowException()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5,
                SecondScore = 6
            };

            // Act
            var frameScore = currentFrame.CalculateScore();
        }

        [TestCategory("Frame")]
        [TestMethod]        
        public void CalculateFirstScoreShouldReturnOpenScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5,
                SecondScore = 4
            };

            // Act
            var frameScore = currentFrame.CalculateScore();

            // Assert
            Assert.AreEqual((5 + 4), frameScore);
            Assert.AreEqual(frameScore, currentFrame.TotalScore);
            Assert.AreEqual(FrameMark.Open, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void CalculateFirstScoreShouldReturnStrikeScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 10,
                SecondScore = 0
            };

            // Act
            var frameScore = currentFrame.CalculateScore();

            // Assert
            Assert.AreEqual(0, frameScore);
            Assert.AreEqual(frameScore, currentFrame.TotalScore);
            Assert.AreEqual(FrameMark.Strike, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void CalculateFirstScoreShouldReturnSpareScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5,
                SecondScore = 5
            };

            // Act
            var frameScore = currentFrame.CalculateScore();

            // Assert
            Assert.AreEqual(0, frameScore);
            Assert.AreEqual(frameScore, currentFrame.TotalScore);
            Assert.AreEqual(FrameMark.Spare, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void OpenScoreShouldSumOpenScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 5,
                SecondScore = 2,
                PreviousFrame = new Frame
                {
                    TotalScore = 7
                }
            };

            // Act
            var frameScore = currentFrame.CalculateScore();

            // Assert
            Assert.AreEqual((7 + 5 + 2), frameScore);
            Assert.AreEqual(frameScore, currentFrame.TotalScore);
            Assert.AreEqual(FrameMark.Open, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void OpenScoreShouldSumPreviousStrikeScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 4,
                SecondScore = 4,
                PreviousFrame = new Frame
                {
                    FirstScore = 10,
                    SecondScore = 0
                }
            };

            // Act
            var frameScore = currentFrame.CalculateScore();

            // Assert
            Assert.AreEqual((10 + 8 + 8), frameScore);
            Assert.AreEqual(frameScore, currentFrame.TotalScore);
            Assert.AreEqual(FrameMark.Open, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void StrikeScoreShouldSumPreviousStrikeScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 10,
                SecondScore = 0,
                PreviousFrame = new Frame
                {
                    FirstScore = 10,
                    SecondScore = 0
                }
            };

            // Act
            var frameScore = currentFrame.CalculateScore();

            // Assert
            Assert.AreEqual((10 + 10 + 10), frameScore);
            Assert.AreEqual(frameScore, currentFrame.TotalScore);
            Assert.AreEqual(FrameMark.Strike, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void SpareScoreShouldSumPreviousSpareScore()
        {
            // Arrange
            var currentFrame = new Frame
            {
                FirstScore = 8,
                SecondScore = 2,
                PreviousFrame = new Frame
                {
                    FirstScore = 4,
                    SecondScore = 6
                }
            };

            // Act
            var frameScore = currentFrame.CalculateScore();

            // Assert
            Assert.AreEqual((10 + 8 + 10), frameScore);
            Assert.AreEqual(frameScore, currentFrame.TotalScore);
            Assert.AreEqual(FrameMark.Spare, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        [ExpectedException(typeof(InvalidScoreException), "InvalidScoreException not thrown")]
        public void LastRoundShouldCalculateFrameShouldThrowInvalidScoreException()
        {
            // Arrange
            var currentFrame = new LastFrame
            {
                FirstScore = 5,
                SecondScore = 7,
                ThirdScore = 3
            };

            // Act
            var frameScore = currentFrame.CalculateOpenFrame();
        }

        [TestCategory("Frame")]
        [TestMethod]
        public void LastRoundShouldSumAsOpenFrame()
        {
            // Arrange
            var currentFrame = new LastFrame
            {
                FirstScore = 5,
                SecondScore = 5,
                ThirdScore = 3
            };

            // Act
            var score = currentFrame.CalculateOpenFrame();

            // Assert
            Assert.AreEqual((5 + 5 + 3), score);
            Assert.AreEqual(FrameMark.Spare, currentFrame.FrameMark);
        }

        [TestCategory("Frame")]
        [TestMethod]
        [ExpectedException(typeof(InvalidScoreException), "InvalidScoreException not thrown")]
        public void LastRoundCalculateScoreShouldThrowInvalidScoreException()
        {
            // Arrange
            var currentFrame = new LastFrame();
            currentFrame.FirstScore = 9;
            currentFrame.SecondScore = 2;
            currentFrame.ThirdScore = 10;
            

            // Act
            var score = currentFrame.CalculateScore();
        }
    }
}
