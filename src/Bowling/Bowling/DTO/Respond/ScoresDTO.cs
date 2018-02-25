using Bowling.Game.Interfaces;
using Bowling.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling.Api.DTO.Respond
{
    public class ScoresDTO
    {
        public int TotalScore { get; set; }
        public List<FrameScoreDTO> Frames { get; set; }

        public ScoresDTO(List<IFrame> frames, int totalScore)
        {
            TotalScore = totalScore;

            Frames = new List<FrameScoreDTO>();
            foreach (var frame in frames)
            {
                if (frame.GetType() == typeof(Frame))
                {
                    Frames.Add(new FrameScoreDTO
                    {
                        FirstScore = frame.FirstScore,
                        SecondScore = frame.SecondScore,
                        ThirdScore = 0,
                        TotalScore = frame.TotalScore
                    });
                }
                else
                {
                    var lastFrame = (LastFrame)frame;
                    Frames.Add(new FrameScoreDTO
                    {
                        FirstScore = lastFrame.FirstScore,
                        SecondScore = lastFrame.SecondScore,
                        ThirdScore = lastFrame.ThirdScore,
                        TotalScore = lastFrame.TotalScore
                    });
                }
            }
        }
    }
}
