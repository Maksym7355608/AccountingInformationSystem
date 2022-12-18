using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.Administration.Infrastructure;
using AccountingInformationSystem.Administration.Infrastructure.Exceptions;
using AccountingInformationSystem.Administration.Interfaces;
using AccountingInformationSystem.Data.EF;
using AccountingInformationSystem.Data.Entities;
using AccountingInformationSystem.Templates.Helpers;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AccountingInformationSystem.Administration.Services
{
    public class AuthService : IAuthService
    {
        private readonly AccountingInformationSystemContext _sqlContext;
        private readonly IMapper _mapper;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthService(AccountingInformationSystemContext context, IMapper mapper, IOptions<AuthOptions> authOptions)
        {
            _sqlContext = context;
            _mapper = mapper;
            _authOptions = authOptions;
        }

        #region Main
        public async Task AddNewUserAsync(UserDataModel user)
        {
            ValidateInput(user);

            var mappedModel = _mapper.Map<User>(user);
            mappedModel.Password = Encrypt(user.Password);
            await _sqlContext.Users.AddAsync(mappedModel);
            await _sqlContext.SaveChangesAsync();
        }

        public async Task<string> LogInAsync(string username, string password)
        {
            var user = await FindCustomerByEmailAsync(username);
            if (user == null)
                throw new AuthorizeException("User not found", HttpStatusCode.Unauthorized);
            if (password != Decrypt(user.Password))
                throw new AuthorizeException("Password is not correct", HttpStatusCode.Unauthorized);
            else
                return GetToken(user);
        }

        public async Task UpdateUserInfoAsync(UserDataModel user)
        {
            ValidateInput(user);

            var mappedModel = _mapper.Map<User>(user);
            _sqlContext.Users.Update(mappedModel);
            await _sqlContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = _sqlContext.Users.FirstOrDefault(x => x.IdentificationNumber == id);
            if (user == default)
                throw new KeyNotFoundException($"User with id: {id} not found");

            _sqlContext.Users.Remove(user);
            await _sqlContext.SaveChangesAsync();
        }
        #endregion

        #region Create Token
        private Task<User> FindCustomerByEmailAsync(string username)
        {
            return Task.Run(() => _sqlContext.Users.FirstOrDefault(x => x.Login == username));
        }

        private string GetToken(User user)
        {
            var authParams = _authOptions.Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = authParams.GetSymmetricSecurityKey();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.IdentificationNumber.ToString()),
                }),
                Expires = DateTime.UtcNow.AddSeconds(authParams.TokenLifeTime),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string Encrypt(string password)
        {
            var data = Encoding.Unicode.GetBytes(password);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string password)
        {
            var data = Convert.FromBase64String(password);
            var decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }
        #endregion

        #region Validation registration
        private void ValidateInput(UserDataModel user)
        {
            if (CheckInvalidName(user.Name, user.Surname, user.Fatherly))
                throw new InvalidInputException("You input inccorrect name/surname/fatherly data");
            if (CheckDepartament(user.Departament))
                throw new InvalidInputException("Departament exist");
            if (CheckUnit(user.Departament, user.Unit))
                throw new InvalidInputException("Unit exist");
            if (CheckPosition(user.Unit, user.Position))
                throw new InvalidInputException("Position exist");
        }

        private bool CheckInvalidName(string name, string surname, string fatherly)
        {
            return StringInputChecker(name) || StringInputChecker(surname) || StringInputChecker(fatherly);
        }

        private bool StringInputChecker(string str)
        {
            return string.IsNullOrEmpty(str) || char.IsLower(str[0]) || str.Any(x => char.IsDigit(x));
        }

        private bool CheckDepartament(string departament)
        {
            return !ValueHelper.Departaments.Any(x => x.name == departament);
        }

        private bool CheckUnit(string departament, string unit)
        {
            var departamentId = ValueHelper.Departaments.FirstOrDefault(x => x.name == departament).id;
            return !ValueHelper.Units.Where(x => x.departamentId == departamentId).Any(x => x.name == unit);
        }

        private bool CheckPosition(string unit, string position)
        {
            var unitId = ValueHelper.Units.FirstOrDefault(x => x.name == unit).id;
            return !ValueHelper.Units.Where(x => x.id == unitId).Any(x => x.name == position);
        }
        #endregion

    }
}
