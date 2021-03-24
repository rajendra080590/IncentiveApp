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
    public class PayrollController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<DashboardController> _logger;
        public PayrollController(IUnitOfWork unitOfWork, IEmailSender emailSender, ILogger<DashboardController> logger)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _logger = logger;
        }


        [HttpPost("GetPayrollDownloadDetails")]
        public IEnumerable<PayrollPending> GetPayrollDownloadDetails(PayrollPending payrollPending)
        {
            _logger.LogInformation("GetPayrollDownloadDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@StatusId", payrollPending.StatusId);
            parameter.Add("@Option", payrollPending.Option);
            parameter.Add("@BranchId", payrollPending.BranchId);
            parameter.Add("@WeekId", payrollPending.WeekId);
            parameter.Add("@PayGroup", payrollPending.PayGroupCode);
            parameter.Add("@RegionCode", payrollPending.RegionCode);
            parameter.Add("@StateCode", payrollPending.StateCode);

            var allObj = _unitOfWork.SP_Call.List<PayrollPending>(SD.Proc_FBMGetEmployeeDetailsPendingByPayroll, parameter);
            _logger.LogInformation("GetPayrollDownloadDetails Completed");
            return allObj.ToList();
        }

        [HttpPost("GetPayrollReviewDetails")]
        public IEnumerable<PayrollReview> GetPayrollReviewDetails(PayrollReview payrollReview)
        {
            _logger.LogInformation("GetPayrollReviewDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@Option", payrollReview.Option);
            parameter.Add("@WeekId", payrollReview.WeekId);
            parameter.Add("@StateCode", payrollReview.StateCode);
            parameter.Add("@RegionCode", payrollReview.RegionCode);
            parameter.Add("@BranchId", payrollReview.BranchId);
            parameter.Add("@PayGroup", payrollReview.PayGroup);

            var allObj = _unitOfWork.SP_Call.List<PayrollReview>(SD.Proc_FBMGetPayrollReviewDetails, parameter);
            _logger.LogInformation("GetPayrollReviewDetails Completed");
            return allObj.ToList();
        }

        [HttpGet("GetWeekIdDetails")]
        public IActionResult GetWeekIdDetails()
        {
            _logger.LogInformation("GetWeekIdDetails Initiated");
            var parameter = new DynamicParameters();
            var allObj = _unitOfWork.SP_Call.List<IncentiveAppCalendar>(SD.Proc_FBMWeekIdDetails, parameter);
            _logger.LogInformation("GetWeekIdDetails Completed");
            return Ok(allObj);
        }

        [HttpPost("UpsertPayrollReviewed")]
        public IActionResult UpsertPayrollReviewed(IEnumerable<PayrollReviewed> payrollReviewed)
        {
            _logger.LogInformation("UpsertPayrollReviewed Initiated");
            foreach (var item in payrollReviewed)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@WeekId", item.WeekId);
                parameter.Add("@PayrollDate", item.PayrollDate);
                parameter.Add("@BranchId", item.BranchId);
                parameter.Add("@FormHeaderId", item.FormHeaderId);
                parameter.Add("@Comments", item.Comments);
                parameter.Add("@SubmittedBy", item.SubmittedBy);
                parameter.Add("@StatusId", item.StatusId);
                _unitOfWork.SP_Call.Execute(SD.Proc_FBMInsertUpdatePayrollReviewed, parameter);
            }
            _unitOfWork.Save();
            _logger.LogInformation("UpsertPayrollReviewed Completed");
            return Ok(200);
        }
    }
}
