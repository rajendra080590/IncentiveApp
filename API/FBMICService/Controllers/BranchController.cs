using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FBMICService.DataAccess.Repository.IRepository;
using FBMICService.Models;
using FBMICService.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FBMICService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BranchController> _logger;
        public BranchController(IUnitOfWork unitOfWork, ILogger<BranchController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("GetBranchDetails/{UserId}")]
        public IActionResult GetBranchDetails(int UserId)
        {
            _logger.LogInformation("GetBranchDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", UserId);
            var allObj = _unitOfWork.SP_Call.List<BranchMaster>(SD.Proc_FBMGetBranchDetails, parameter);
            _logger.LogInformation("GetBranchDetails Completed");
            return Ok(allObj);
        }

        [HttpGet("GetAllBranches/{option}/{stateCode}")]
        public IActionResult GetAllBranches(int? option, string stateCode)
        {
            _logger.LogInformation("GetAllBranches Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@Option", option);
            parameter.Add("@StateCode", stateCode);
            var allObj = _unitOfWork.SP_Call.List<BranchMaster>(SD.Proc_FBMGetAllBranches, parameter);
            _logger.LogInformation("GetAllBranches Completed");
            return Ok(allObj);
        }

        [HttpGet("GetAllPayGroups")]
        public IActionResult GetAllPayGroups()
        {
            _logger.LogInformation("GetAllPayGroups Initiated");
            var parameter = new DynamicParameters();
            //parameter.Add("@UserId", UserId);
            var allObj = _unitOfWork.SP_Call.List<PayGroup>(SD.Proc_FBMGetAllPayGroup, parameter);
            _logger.LogInformation("GetAllPayGroups Completed");
            return Ok(allObj);
        }


        [HttpGet("GetAllStates/{option}/{regionCode}")]
        public IActionResult GetAllStates(int? option, int? regionCode)
        {
            _logger.LogInformation("GetAllStates Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@Option", option);
            parameter.Add("@RegionCode", regionCode);
            var allObj = _unitOfWork.SP_Call.List<States>(SD.Proc_FBMGetAllStates, parameter);
            _logger.LogInformation("GetAllStates Completed");
            return Ok(allObj);
        }

        [HttpGet("GetAllRegions/{option}/{stateCode}")]
        public IActionResult GetAllRegions(int? option, string stateCode = null)
        {
            _logger.LogInformation("GetAllRegions Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@Option", option);
            parameter.Add("@StateCode", stateCode);
            var allObj = _unitOfWork.SP_Call.List<Region>(SD.Proc_FBMGetAllRegions, parameter);
            _logger.LogInformation("GetAllRegions Completed");
            return Ok(allObj);
        }


        [HttpGet("GetAllPrimaryJobs/{branchId}")]
        public IActionResult GetAllPrimaryJobs(string branchId)
        {
            _logger.LogInformation("GetAllPrimaryJobs Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@BranchId", branchId);
            var allObj = _unitOfWork.SP_Call.List<PrimaryJobs>(SD.Proc_FBMGetPrimaryJobs, parameter);
            _logger.LogInformation("GetAllPrimaryJobs Completed");
            return Ok(allObj);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            //throw new Exception();
            return "New York";
        }
    }
}
