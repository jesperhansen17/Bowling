using System.Collections.Generic;
using System.Linq;
using System.Net;
using Bowling.Api.DTO.Respond;
using Bowling.Api.ViewModels;
using Bowling.Game.Exceptions;
using Bowling.Game.Interfaces;
using Bowling.Game.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Controllers
{
    public class BowlingController : Controller
    {
        private readonly IGame _bowlingGame;

        public BowlingController(IGame bowlingGame)
        {
            _bowlingGame = bowlingGame;
        }

        [HttpPost]
        [Route("api/score")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CalculateScore([FromBody]FramesDTO ScoreRequest)
        {
            if (ScoreRequest.Frames.Count() < 1)
            {
                return BadRequest();
            }

            for (var i = 0; i < ScoreRequest.Frames.Count; i++)
            {
                if (i != ScoreRequest.Frames.Count - 1)
                {
                    _bowlingGame.Frames.Add(new Frame
                    {
                        FirstScore = ScoreRequest.Frames[i].First,
                        SecondScore = ScoreRequest.Frames[i].Second
                    });
                }
                else
                {
                    _bowlingGame.Frames.Add(new LastFrame
                    {
                        FirstScore = ScoreRequest.Frames[i].First,
                        SecondScore = ScoreRequest.Frames[i].Second,
                        ThirdScore = ScoreRequest.Frames[i].Third.HasValue ? ScoreRequest.Frames[i].Third.Value : 0
                    });
                }
            }

            // Calculate Score
            try
            {
                _bowlingGame.CalculateGameScore();
            }
            catch (InvalidScoreException ex)
            {
                return BadRequest();
            }

            var totalScore = _bowlingGame.GetTotalScore();
            var frames = _bowlingGame.Frames;

            var results = new ScoresDTO(frames, totalScore);

            return Ok(results);
        }
    }
}
