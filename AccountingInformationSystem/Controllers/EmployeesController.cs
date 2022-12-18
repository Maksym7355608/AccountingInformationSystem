using AccountingInformationSystem.Administration.CreateObjects;
using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.Administration.Interfaces;
using AccountingInformationSystem.CreateCommands;
using AccountingInformationSystem.Filters;
using AccountingInformationSystem.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingInformationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AdminExceptionFilter]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IAdminsService _service;
        private readonly IMapper _mapper;

        public EmployeesController(IAdminsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _service.GetEmployees().ToList();
            return Ok(_mapper.Map<List<EmployeesViewModel>>(employees));
        }

        [HttpGet]
        public IActionResult GetEmployees([FromQuery] EmployeeFilterCreateCommand cmd)
        {
            var filter = new AdministrationCreateObject
            {
                EmployeeId = cmd.EmployeeId,
                OrganizationDepartament = cmd.OrganizationDepartament,
                OrganizationUnit = cmd.OrganizationUnit,
                Position = cmd.Position
            };
            var employees = _service.GetEmployeesByFilters(filter).ToList();
            return Ok(_mapper.Map<List<EmployeesViewModel>>(employees));
        }

        [HttpGet("id")]
        public IActionResult GetEmployeesById(long id)
        {
            var employee = _service.GetEmployeeById(id);
            return Ok(_mapper.Map<EmployeesViewModel>(employee));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromQuery] AddEmployeeCreateCommand cmd)
        {
            var employee = _mapper.Map<EmployeeDataModel>(cmd);
            await _service.AddNewEmployeeAsync(employee);
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeAsync([FromQuery] UpdateEmployeeCreateCommand cmd)
        {
            var employee = _mapper.Map<EmployeeDataModel>(cmd);

            if (cmd.TransferDate.HasValue)
                employee.EmploymentDate = cmd.TransferDate.Value;

            await _service.AddNewEmployeeAsync(employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(long id)
        {
            await _service.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
