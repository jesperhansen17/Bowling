using System.Collections.Generic;
using System.Linq;
using System.Net;
using Bowling.Api.DTO.Respond;
using Bowling.Api.ViewModels;
using Bowling.Core.Logger;
using Bowling.Game.Exceptions;
using Bowling.Game.Interfaces;
using Bowling.Game.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bowling.Controllers
{
    public class BowlingController : Controller
    {
        private readonly IGame _bowlingGame;
        private readonly ILogger _logger;

        public BowlingController(IGame bowlingGame, ILogger<BowlingController> logger)
        {
            _bowlingGame = bowlingGame;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/score")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CalculateScore([FromBody]FramesDTO ScoreRequest)
        {
            _logger.LogInformation(LogEvents.SubmitScore, "Calculating score");
            if (ScoreRequest.Frames.Count() < 1)
            {
                _logger.LogWarning(LogEvents.NoFramesPost, "Bad Request: No frames posted");
                return BadRequest();
            }
            if (ScoreRequest.Frames.Count() > 10)
            {
                _logger.LogWarning(LogEvents.ToManyFramesPost, "Bad Request: To many frames posted");
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
            _bowlingGame.CalculateGameScore();
            var totalScore = _bowlingGame.GetTotalScore();
            var frames = _bowlingGame.Frames;

            var results = new ScoresDTO(frames, totalScore);

            return Ok(results);
        }
    }
}
