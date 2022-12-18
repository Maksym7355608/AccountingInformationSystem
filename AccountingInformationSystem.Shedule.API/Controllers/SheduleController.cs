using AccountingInformationSystem.Shedules.DataModels;
using AccountingInformationSystem.Shedules.API.CreateCommands;
using AccountingInformationSystem.Shedules.API.ViewModels;
using AccountingInformationSystem.Shedules.CreateObjects;
using AccountingInformationSystem.Shedules.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AccountingInformationSystem.Shedules.API.Filters;

namespace AccountingInformationSystem.Shedules.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SheduleExceptionFilter]
    public class SheduleController : ControllerBase
    {
        private readonly ISheduleService _sheduleService;
        private readonly IMapper _mapper;

        public SheduleController(ISheduleService shedule, IMapper mapper)
        {
            _sheduleService = shedule;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetShedulesByFilters([FromQuery] SheduleCreateCommand cmd)
        {
            var createObject = new SheduleCreateObject
            {
                EmployeeId = cmd.EmployeeId,
                OrganizationDepartament = cmd.OrganizationDepartament,
                OrganizationUnit = cmd.OrganizationUnit,
                DateFrom = cmd.DateFrom,
                DateTo = cmd.DateTo ?? DateTime.Now
            };

            var shedules = _mapper.Map<List<WorkSheduleViewModel>>(_sheduleService.GetWorkShedulesByFilters(createObject).ToList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(shedules);
        }

        [HttpPost]
        public IActionResult AddNewShedules(List<WorkSheduleViewModel> newShedules)
        {
            var mappedShedules = _mapper.Map<List<WorkSheduleDataModel>>(newShedules);

            _sheduleService.AddShedulesAsync(mappedShedules);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateShedules(List<WorkSheduleViewModel> newShedules)
        {
            var mappedShedules = _mapper.Map<List<WorkSheduleDataModel>>(newShedules);

            _sheduleService.UpdateShedulesAsync(mappedShedules);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}