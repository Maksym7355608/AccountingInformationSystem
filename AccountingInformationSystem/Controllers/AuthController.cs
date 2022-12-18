using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.Administration.Interfaces;
using AccountingInformationSystem.CreateCommands;
using AccountingInformationSystem.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountingInformationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AdminExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService service, IMapper mapper)
        {
            _authService = service;
            _mapper = mapper;
        }

        [HttpGet("login")]
        public async Task<IActionResult> SignInAsync([FromBody] string username, [FromBody] string password)
        {
            var token = await _authService.LogInAsync(username, password);

            return Ok(token);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> SignUpAsync([FromQuery] RegistrationCreateCommand cmd)
        {
            var newUser = _mapper.Map<UserDataModel>(cmd);

            await _authService.AddNewUserAsync(newUser);
            return Ok();
        }

        [HttpPost("settings")]
        public async Task<IActionResult> UpdateAsync([FromQuery] UpdateUserCreateCommand cmd)
        {
            var newUser = _mapper.Map<UserDataModel>(cmd);

            await _authService.UpdateUserInfoAsync(newUser);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SignUpAsync(long id)
        {
            await _authService.DeleteUserAsync(id);
            return Ok();
        }

    }
}
