using Lesson19Test.DataBase.Model.DatabaseModels;
using Lesson19Test.DataBase.Model.DTO;
using Lesson19Test.DataBase.Repositories;
using Lesson19Test.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public ResponseDto Login(string username, string password)
        {
            var user = _repository.GetUser(username);
            if (user is null)
                return new ResponseDto(false, "Username or password does not match");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new ResponseDto(false, "Username or password does not match");

            string token = CreateToken(user);
            return new ResponseDto(true, token);
        }

        public ResponseDto Signup(string username, string password)
        {
            var user = _repository.GetUser(username);
            if (user is not null)
                return new ResponseDto(false, "User already exists");

            user = CreateUser(username, password);
            _repository.SaveUser(user);
            return new ResponseDto(true);
        }

        private User CreateUser(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                categories = new List<Category>()
            };

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }

        private string CreateToken(User user)
        {
            List<Claim> claimse = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var secretToken = _configuration.GetSection("JWT:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claimse,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
