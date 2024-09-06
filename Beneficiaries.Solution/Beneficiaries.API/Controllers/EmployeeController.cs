using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beneficiaries.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Add([FromBody] EmployeeDTO employee)
        {
            if (employee == null)
            {
                _logger.LogError("Employee data must be provided!");
                return BadRequest("Employee data is null");
            }

            var newEmployeeId = await _employeeService.Add(employee);
            return Ok(newEmployeeId);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Update([FromBody] EmployeeDTO employee)
        {
            if (employee == null || employee.ID == 0)
            {
                _logger.LogError("Employee data or ID must be provided!");
                return BadRequest("Invalid employee data or ID");
            }

            var result = await _employeeService.Update(employee);
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
                return BadRequest("Invalid ID");
            }

            var result = await _employeeService.Delete(id);
            return Ok(result);
        }

        [HttpGet("ObtAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<object>>> ObtAll(int page = 1, int sizePage = 10, string sorting = "Id")
        {
            _logger.LogInformation("Get list");
            var LItems = await _employeeService.ObtAll(page, sizePage, sorting);
            return Ok(LItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public object GetById(Int64 id)
        {
            if (id == 0)
            {
                _logger.LogError("Must send the ID!");
            }

            object Item = _employeeService.ObtXId(id);

            return Item;
        }
    }
}
