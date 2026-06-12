using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.OpenApi.Extensions;
using RpsslGameApi.Contracts;
using Results = RpsslGameApi.Domain.Entities.Results;

namespace RpsslGameApi.Application.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/game")]
    public class RpsslGameController : ControllerBase
    {
        public  RpsslGameController()
        {
        }
     
        [HttpGet("choices")]
        public async Task<ActionResult<IEnumerable<ChoiceResponse>>> GetChoices()
        {
            return Ok(new List<ChoiceResponse> { ChoiceResponse.FromId(1), ChoiceResponse.FromId(2)});
        }
     
        [HttpGet("choice")]
        public async Task<ActionResult<ChoiceResponse>> GetChoice()
        {
            return Ok(ChoiceResponse.FromId(2));
        }
     
        [HttpPost("play")]
        public async Task<ActionResult<PlayResponse>> Play(string input)
        {
            return Ok(new PlayResponse()
            {
                Computer = 1,
                Player = 1,
                Result = Results.Tie.ToString()
                
            });
        }
    }
}