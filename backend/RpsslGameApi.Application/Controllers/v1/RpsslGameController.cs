using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Services.Interfaces;

namespace RpsslGameApi.Application.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class RpsslGameController : ControllerBase
    {   
        private readonly IGetChoiceService _choiceService;
        private readonly IGetChoicesService _choicesService;
        private readonly IPlayService _playService;
        private readonly IScoreboardService _scoreboardService;

        public RpsslGameController(
            IGetChoiceService choiceService,
            IGetChoicesService choicesService,
            IPlayService playService,
            IScoreboardService scoreboardService
        )
        {
            _choiceService = choiceService;
            _choicesService = choicesService;
            _playService = playService;
            _scoreboardService = scoreboardService;
        }
     
        [HttpGet("choices")]
        public async Task<ActionResult<IEnumerable<ChoiceResponse>>> GetChoices()
        {
            return Ok(_choicesService.FetchChoices());
        }
     
        [HttpGet("choice")]
        public async Task<ActionResult<ChoiceResponse>> GetChoice()
        {
            return Ok(await _choiceService.FetchChoice());
        }
     
        [HttpPost("play")]
        public async Task<ActionResult<PlayResponse>> Play([FromBody]PlayRequest request)
        {
            var player = request.Player; 
            if(1 > player ||  player > 5)
                return BadRequest("The choice must be between 1 and 5");
            return Ok(await _playService.PlayGame(player));
        }
        
        [HttpGet("scoreboard")]
        public async Task<ActionResult<IEnumerable<PlayResponse>>> Scoreboard()
        {
            
            return Ok(_scoreboardService.GetResults());
        }

        [HttpDelete("scoreboard")]
        public async Task<ActionResult<bool>> DeleteScoreboard()
        {
            return Ok(_scoreboardService.Clear());
        }
    }
}