using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FBMICService.DataAccess.Repository.IRepository;
using FBMICService.Interfaces;
using FBMICService.Models;
using FBMICService.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FBMICService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoginController> _logger;

        //Native Login Code Used for Production
        public LoginController(IUnitOfWork unitOfWork, ILogger<LoginController> logger, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetById/{userid}")]
        public IActionResult GetById(int userid)
        {
            var response = _userService.GetById(userid);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("ValidateUser")]
        public IActionResult ValidateUser(FBMUsers fBMUsers)
        {
            //_logger.LogInformation("Validate User Initiated");
            //var result = _unitOfWork.Login.GetFirstOrDefault(u => u.UserName == fBMUsers.UserName && u.Password == fBMUsers.Password && u.Status == true);
            //_logger.LogInformation("Validate User Completed");

            _logger.LogInformation("Validate User Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@EmailId", fBMUsers.EmailId);
            parameter.Add("@UserId", fBMUsers.UserId);
            parameter.Add("@UserName", fBMUsers.UserName);
            parameter.Add("@Password", fBMUsers.Password);
            var result = _unitOfWork.SP_Call.List<FBMUsers>(SD.Proc_FBMValidateUser, parameter);
            _logger.LogInformation("Validate User Completed");

            return Ok(result);
        }

        [HttpPost("ValidateUserId/{userId}")]
        public IActionResult ValidateUserId(int userId)
        {
            _logger.LogInformation("Validate User with ID Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", userId);
            var result = _unitOfWork.SP_Call.List<FBMUsers>(SD.Proc_FBMValidateUser, parameter);
            _logger.LogInformation("Validate User with ID Completed");

            return Ok(result);

            //var result = _unitOfWork.Login.GetFirstOrDefault(u => u.UserId == userId &&  u.Status == true);
            //_logger.LogInformation("Validate User with ID Completed");
            //return Ok(result);
        }


        //[Authorize]
        [HttpPost("ValidateADUser/{userEmail}")]
        public IActionResult ValidateADUser(string userEmail)
        {
            //_logger.LogInformation("Validate AD User with Email Initiated");
            //var result = _unitOfWork.Login.GetFirstOrDefault(u => u.EmailId == userEmail && u.Status == true);
            //_logger.LogInformation("Validate AD User with Email Initiated");
            //return Ok(result);


            _logger.LogInformation("Validate AD User with Email Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@EmailId", userEmail);
            var result = _unitOfWork.SP_Call.List<FBMUsers>(SD.Proc_FBMValidateUser, parameter);
            _logger.LogInformation("Validate AD User with Email Initiated");

            return Ok(result);
        }
    }
}
