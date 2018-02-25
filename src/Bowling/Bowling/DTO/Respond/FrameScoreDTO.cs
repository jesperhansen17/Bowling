using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling.Api.DTO.Respond
{
    public class FrameScoreDTO
    {
        public int FirstScore { get; set; }
        public int SecondScore { get; set; }
        public int ThirdScore { get; set; }
        public int TotalScore { get; set; }
    }
}
