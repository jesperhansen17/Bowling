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

        public virtual int CalculateScore()
        {
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

        public virtual int CalculateOpenFrame()
        {
            return FirstScore + SecondScore;
        }

        public int CalculateSpare(IFrame frame)
        {
            // Update previous frames score
            frame.TotalScore = frame.PreviousFrame.TotalScore + 10 + FirstScore;
            return frame.TotalScore;
        }

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

        public override int CalculateScore()
        {
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

        public override int CalculateOpenFrame()
        {
            return FirstScore + SecondScore + ThirdScore;
        }
    }
}
