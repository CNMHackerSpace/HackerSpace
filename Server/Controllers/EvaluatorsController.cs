using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IEnumerable<Evaluator>> GetEvaluatorsAsync()
        {
            _logger.Log(LogLevel.Information, "GetEvaluators Executed.");
            return await _evaluatorsRepo.GetAllAsync();
        }

        [Authorize(Roles = "admin, badgecreator")]
        [HttpPost]
        public async Task<ActionResult> AddEvaluator([FromBody] Evaluator evaluator)
        {
            _logger.Log(LogLevel.Information, "AddOrUpdateEvaluators Executed.");
            //TODO: Check to see if email corresponds to an existing user account. RJG
            await _evaluatorsRepo.AddEvaluatorAsync(evaluator);


            try
            {
                return StatusCode(StatusCodes.Status200OK);

                //if (employeeToUpdate == null)
                //    return NotFound($"Employee with Id = {id} not found");

                //return await employeeRepository.UpdateEmployee(employee);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding evaluator");
            }
        }
    }
}
