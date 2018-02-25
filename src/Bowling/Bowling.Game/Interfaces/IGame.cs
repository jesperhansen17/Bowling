using Bowling.Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Game.Interfaces
{
    public interface IGame
    {
        List<IFrame> Frames { get; set; }
        void CalculateGameScore();
        int GetTotalScore();
    }
}
