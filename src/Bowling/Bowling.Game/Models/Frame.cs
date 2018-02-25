using Bowling.Game.Exceptions;
using Bowling.Game.Interfaces;

namespace Bowling.Game.Models
{
    public class Frame : IFrame
    {
        protected const int STRIKE_SPARE_POINTS = 10;
        public IFrame PreviousFrame { get; set; }
        public int FirstScore { get; set; }
        public int SecondScore { get; set; }
        public int TotalScore { get; set; }
        public FrameMark FrameMark
        {
            get
            {
                if (FirstScore == STRIKE_SPARE_POINTS)
                {
                    return FrameMark.Strike;
                }
                else if (FirstScore + SecondScore == STRIKE_SPARE_POINTS)
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
            if (FirstScore + SecondScore > STRIKE_SPARE_POINTS)
            {
                throw new InvalidScoreException("The sum of first and second score is greater than 10");
            }

            if (PreviousFrame != null)
            {
                if (PreviousFrame.FrameMark == FrameMark.Open)
                {
                    return TotalScore = PreviousFrame.TotalScore + CalculateOpenFrame();
                }
                else
                {
                    var previousFrameScore = CalculatePreviousFrame();
                    var currentFrameScore = CalculateOpenFrame();
                    return TotalScore = previousFrameScore + currentFrameScore;
                }
            }
            else
            {
                if (FrameMark == FrameMark.Open)
                {
                    return TotalScore = CalculateOpenFrame();
                }
                else
                {
                    return TotalScore = 0;
                }
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
        /// Calculates the total score for the previous spare frame.
        /// </summary>
        /// <param name="spareFrame">The frame to calculate.</param>
        /// <returns>The total score for an spare frame</returns>
        public int CalculatePreviousSpare(IFrame spareFrame)
        {
            if (spareFrame.PreviousFrame != null)
            {
                spareFrame.TotalScore = spareFrame.PreviousFrame.TotalScore + STRIKE_SPARE_POINTS + FirstScore;
            }
            else
            {
                spareFrame.TotalScore = STRIKE_SPARE_POINTS + FirstScore;
            }         
            return spareFrame.TotalScore;
        }

        /// <summary>
        /// Calculates the total score for the previous strike frame.
        /// </summary>
        /// <param name="strikeFrame">The frame to calculate.</param>
        /// <returns>The total score for an strike frame</returns>
        public int CalculatePreviousStrike(IFrame strikeFrame)
        {
            if (strikeFrame.PreviousFrame != null)
            {
                strikeFrame.TotalScore = strikeFrame.PreviousFrame.TotalScore + STRIKE_SPARE_POINTS + FirstScore + SecondScore;
            }
            else
            {
                strikeFrame.TotalScore = STRIKE_SPARE_POINTS + FirstScore + SecondScore;
            }
            return strikeFrame.TotalScore;
        }

        protected int CalculatePreviousFrame()
        {
            switch (PreviousFrame.FrameMark)
            {
                case FrameMark.Spare:
                    return CalculatePreviousSpare(PreviousFrame);
                case FrameMark.Strike:
                    return CalculatePreviousStrike(PreviousFrame);
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
                    return TotalScore = PreviousFrame.TotalScore + CalculateOpenFrame();
                }
                else
                {
                    var previousFrameScore = CalculatePreviousFrame();
                    var currentFrameScore = CalculateOpenFrame();
                    return TotalScore = previousFrameScore + currentFrameScore;
                }
            }
            else
            {
                return TotalScore = CalculateOpenFrame();
            }
        }

        /// <summary>
        /// Calculates the total score for the last frame of the game.
        /// </summary>
        /// <returns>Total score for the last frame of the game.</returns>
        public override int CalculateOpenFrame()
        {
            if (FrameMark == FrameMark.Open && ThirdScore != 0)
            {
                throw new InvalidScoreException("Frame mark is open and thirdscore is not 0");
            }

            return FirstScore + SecondScore + ThirdScore;
        }
    }
}
