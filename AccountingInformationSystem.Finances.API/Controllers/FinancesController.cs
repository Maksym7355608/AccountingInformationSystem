using AccountingInformationSystem.Finances.API.CreateCommands;
using AccountingInformationSystem.Finances.API.ViewModels;
using AccountingInformationSystem.Finances.CreateObject;
using AccountingInformationSystem.Finances.Interfaces;
using AccountingInformationSystem.Templates.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountingInformationSystem.Finances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancesController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        private readonly IMapper _mapper;

        public FinancesController(ISalaryService salaryService, IMapper mapper)
        {
            _salaryService = salaryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CalculatePayments([FromQuery] CalculationPayoutCreateCommand cmd)
        {
            var createObject = new FinancesCreateObject
            {
                EmployeeId = cmd.EmployeeId,
                OrganizationDepartament = cmd.OrganizationDepartament,
                OrganizationUnit = cmd.OrganizationUnit,
                PeriodFrom = cmd.DateFrom.ToPeriod(),
                PeriodTo = cmd.DateTo.HasValue ? cmd.DateTo.Value.ToPeriod() : DateTime.Now.ToPeriod().PreviousPeriod(),
            };

            var resultModel = _mapper.Map<List<FinanceViewModel>>(_salaryService.CalculatePayoutsByFilter(createObject).ToList());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(resultModel);
        }
    }
}
