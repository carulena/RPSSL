using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.OpenApi.Extensions;
using RpsslGameApi.Contracts;
using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Infrastructure.Services.Interfaces;

namespace RpsslGameApi.Application.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/game")]
    public class RpsslGameController : ControllerBase
    {   
        private readonly IGetChoiceService _choiceService;
        private readonly IGetChoicesService _choicesService;
        private readonly IPlayService _playService;

        public RpsslGameController(
            IGetChoiceService choiceService,
            IGetChoicesService choicesService,
            IPlayService playService
        )
        {
            _choiceService = choiceService;
            _choicesService = choicesService;
            _playService = playService;
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
        public async Task<ActionResult<PlayResponse>> Play(int player)
        {
            if(1 > player ||  player > 5)
                return BadRequest("The choice must be between 1 and 5");
            return Ok(await _playService.FetchPlay(player));
        }
    }
}