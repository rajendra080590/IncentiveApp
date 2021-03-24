using FBMICService.Interfaces;
using FBMICService.Models;
using Microsoft.Extensions.Options;
using FBMICService.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using FBMICService.Helpers;

namespace FBMICService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _unitOfWork.Login.GetFirstOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<FBMUsers> GetAll()
        {
            return _unitOfWork.Login.GetAll().ToList();
        }

        public FBMUsers GetById(int userid)
        {
            return _unitOfWork.Login.GetFirstOrDefault(u => u.UserId == userid && u.Status == true);
        }

        private string generateJwtToken(FBMUsers user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userid", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
