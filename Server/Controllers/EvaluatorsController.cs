using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluatorsController : ControllerBase
    {
        private readonly ILogger<EvaluatorsController> _logger;
        private readonly IEvaluatorsRepo _evaluatorsRepo;

        public EvaluatorsController(ILogger<EvaluatorsController> logger, IEvaluatorsRepo evaluatorsRepo)
        {
            _logger = logger;
            _evaluatorsRepo = evaluatorsRepo;
        }

        [Authorize(Roles = "admin, badgecreator")]
        [HttpPut]
        [Route("/AddUpdatedForBadge/{id:int}")]
        public async void AddOrUpdateEvaluators(int id, IEnumerable<string> evaluators)
        {
            _logger.Log(LogLevel.Information, "AddOrUpdateEvaluators Executed.");
            await _evaluatorsRepo.AddEvaluatorsForBadgeAsync(id, evaluators);
        }
        [Authorize(Roles = "admin, badgecreator")]
        [HttpGet]
        public async Task<IEnumerable<Evaluator>> GetEvaluatorsAsync()
        {
            _logger.Log(LogLevel.Information, "GetEvaluators Executed.");
            return await _evaluatorsRepo.GetAllAsync();
        }
    }
}
