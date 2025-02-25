﻿using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Beneficiaries.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly ILogger<BeneficiaryController> _logger;

        public BeneficiaryController(IBeneficiaryService beneficiaryService, ILogger<BeneficiaryController> logger)
        {
            _beneficiaryService = beneficiaryService;
            _logger = logger;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Add([FromBody] BeneficiaryDTO beneficiary)
        {
            if (beneficiary == null)
            {
                _logger.LogError("Beneficiary data must be provided!");
                return BadRequest("No se enviaron datos");
            }

            try
            {
                var newBeneficiaryId = await _beneficiaryService.Add(beneficiary);
                return Ok(newBeneficiaryId);
            }
            catch (SqlException ex) when (ex.Number == 50000 || ex.Number == 2627)
            {
                return Conflict("La suma total de los porcentajes de participación no puede exceder el 100.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting beneficiary: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Update([FromBody] BeneficiaryDTO beneficiary)
        {
            if (beneficiary == null || beneficiary.ID == 0)
            {
                _logger.LogError("Beneficiary data or ID must be provided!");
                return BadRequest("No se enviaron datos");
            }

            var result = await _beneficiaryService.Update(beneficiary);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Delete(Int64 id)
        {
            if (id == 0)
            {
                _logger.LogError("ID must be provided!");
                return BadRequest("ID invalido");
            }

            var result = await _beneficiaryService.Delete(id);
            return Ok(result);
        }

        [HttpGet("ObtAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<object>>> ObtAll(int page = 1, int sizePage = 10, string sorting = "Id")
        {
            _logger.LogInformation("Get list");
            var LItems = await _beneficiaryService.ObtAllDAO(page, sizePage, sorting);
            return Ok(LItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public object ObtXId(Int64 id)
        {
            if (id == 0)
            {
                _logger.LogError("Must send the ID!");
            }

            object Item = _beneficiaryService.ObtXId(id);

            return Item;
        }

        [HttpGet("ObtAllXEmployeeId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<object>>> ObtAllXEmployeeId(Int64 employeeId, int page = 1, int sizePage = 10, string sorting = "Id")
        {
            _logger.LogInformation("Get list x Employee Id");
            var LItems = await _beneficiaryService.ObtAllXEmployeeIdDAO(employeeId, page, sizePage, sorting);
            return Ok(LItems);
        }

        [HttpGet("ValidateParticipation/{employeeId}/{newPercentage}")]
        public async Task<IActionResult> ValidateTotalParticipation([FromRoute] Int64 employeeId, [FromRoute] int newPercentage)
        {
            try
            {
                var isValid = await _beneficiaryService.ValidateTotalParticipation(employeeId, newPercentage);
                return Ok(isValid);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error validating total participation: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
