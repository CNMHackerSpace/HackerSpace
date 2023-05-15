using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluatorsControllers : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private readonly IEvaluatorsRepo _evaluatorsRepo;

        public EvaluatorsControllers(ILogger<BadgesController> logger, IEvaluatorsRepo evaluatorsRepo)
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
    }
}
