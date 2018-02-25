using Bowling.Game.Exceptions;
using Bowling.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Game.Models
{
    public class Frame : IFrame
    {
        public IFrame PreviousFrame { get; set; }
        public int FirstScore { get; set; }
        public int SecondScore { get; set; }
        public int TotalScore { get; set; }
        public FrameMark FrameMark
        {
            get
            {
                if (FirstScore == 10)
                {
                    return FrameMark.Strike;
                }
                else if (FirstScore + SecondScore == 10)
                {
                    return FrameMark.Spare;
                }
                else
                {
                    return FrameMark.Open;
                }
            }
        }

        /// <summary>
        /// Calculates the frame score by adding last frames total score with this frames total score.
        /// If previous frame where a spare/strike and this frame is an open frame it updates previous frame using this frames score.
        /// </summary>
        /// <returns>The total score of the frame</returns>
        public virtual int CalculateScore()
        {            
            if (FirstScore + SecondScore > 10)
            {
                throw new InvalidScoreException("The sum of first and second score is greater than 10");
            }

            if (PreviousFrame != null)
            {
                if (PreviousFrame.FrameMark == FrameMark.Open)
                {
                    return PreviousFrame.TotalScore + CalculateOpenFrame();
                }
                else
                {
                    var previousFrameScore = CalculatePreviousFrame();
                    var currentFrameScore = CalculateOpenFrame();
                    return previousFrameScore + currentFrameScore;
                }
            }
            else
            {
                return CalculateOpenFrame();
            }
        }

        /// <summary>
        /// Calculates the total score of an open frame
        /// </summary>
        /// <returns>Total score for an open frame</returns>
        public virtual int CalculateOpenFrame()
        {
            return FirstScore + SecondScore;
        }

        /// <summary>
        /// Calculates the total score for an spare frame.
        /// </summary>
        /// <param name="frame">The frame to calculate.</param>
        /// <returns>The total score for an spare frame</returns>
        public int CalculateSpare(IFrame frame)
        {
            // Update previous frames score
            frame.TotalScore = frame.PreviousFrame.TotalScore + 10 + FirstScore;
            return frame.TotalScore;
        }

        /// <summary>
        /// Calculates the total score for an strike frame.
        /// </summary>
        /// <param name="frame">The frame to calculate.</param>
        /// <returns>The total score for an strike frame</returns>
        public int CalculateStrike(IFrame frame)
        {
            // Update previous frames score
            frame.TotalScore = frame.PreviousFrame.TotalScore + 10 + FirstScore + SecondScore;
            return frame.TotalScore;
        }

        protected int CalculatePreviousFrame()
        {
            switch (PreviousFrame.FrameMark)
            {
                case FrameMark.Spare:
                    return CalculateSpare(PreviousFrame);
                case FrameMark.Strike:
                    return CalculateStrike(PreviousFrame);
                default:
                    return 0;
            }
        }
    }

    public sealed class LastFrame : Frame
    {
        public int ThirdScore { get; set; }

        /// <summary>
        /// Calculates the frame score by adding last frames total score with this frames total score.
        /// If previous frame where a spare/strike and this frame is an open frame it updates previous frame using this frames score.
        /// </summary>
        /// <returns>The total score of the frame</returns>
        public override int CalculateScore()
        {
            if (FirstScore + SecondScore + ThirdScore > 30)
            {
                throw new InvalidScoreException("The sum of first, second and third score in the last frame is greater than 30");
            }

            if (PreviousFrame != null)
            {
                if (PreviousFrame.FrameMark == FrameMark.Open)
                {
                    return PreviousFrame.TotalScore + CalculateOpenFrame();
                }
                else
                {
                    var previousFrameScore = CalculatePreviousFrame();
                    var currentFrameScore = CalculateOpenFrame();
                    return previousFrameScore + currentFrameScore;
                }
            }
            else
            {
                return CalculateOpenFrame();
            }
        }

        /// <summary>
        /// Calculates the total score for the last frame of the game.
        /// </summary>
        /// <returns>Total score for the last frame of the game.</returns>
        public override int CalculateOpenFrame()
        {
            return FirstScore + SecondScore + ThirdScore;
        }
    }
}
