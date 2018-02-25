using Bowling.Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Game.Interfaces
{
    public interface IFrame
    {
        IFrame PreviousFrame { get; set; }
        int FirstScore { get; set; }
        int SecondScore { get; set; }
        int TotalScore { get; set; }
        FrameMark FrameMark { get; }
        int CalculateScore();
        int CalculateOpenFrame();
        int CalculateSpare(IFrame frame);
        int CalculateStrike(IFrame frame);
    }
}
