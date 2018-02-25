using Bowling.Core.Exceptions;
using Bowling.Game.Interfaces;
using Bowling.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling.Game
{
    public class BowlingGame : IGame
    {
        public List<IFrame> Frames { get; set; }

        public BowlingGame()
        {
            Frames = new List<IFrame>();
        }

        /// <summary>
        /// Calculates the total score of the game.
        /// </summary>
        public void CalculateGameScore()
        {
            ConnectFrames();

            for (var i = 0; i < Frames.Count; i++)
            {
                Frames[i].TotalScore = Frames[i].CalculateScore();
            }
        }

        /// <summary>
        /// Get's the currently highest score
        /// </summary>
        /// <returns>Total Score</returns>
        public int GetTotalScore()
        {
            var frameWithHighestScore = Frames.OrderByDescending(frame => frame.TotalScore).FirstOrDefault();
            
            if (frameWithHighestScore.TotalScore == 0 && Frames.Sum(x => x.FirstScore + x.SecondScore) != 0)
            {
                throw new ScoreNotCalculatedException();
            }

            return frameWithHighestScore.TotalScore;
        }

        /// <summary>
        /// Links all frames together, each frame are linked to its previous frame.
        /// </summary>
        private void ConnectFrames()
        {
            for (var i = 0; i < Frames.Count; i++)
            {
                if (i != 0)
                {
                    Frames[i].PreviousFrame = Frames[i - 1];
                }
            }
        }
    }
}
