using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FBMICService.DataAccess.Repository.IRepository;
using FBMICService.Models;
using FBMICService.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FBMICService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(IUnitOfWork unitOfWork, IEmailSender emailSender, ILogger<DashboardController> logger)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost("GetDashboardDetails")]
        public IEnumerable<DashboardDetails> GetDashboardDetails(DashboardRequest dashboardReq)
        {
            _logger.LogInformation("GetDashboardDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", dashboardReq.UserId);
            parameter.Add("@RoleId", dashboardReq.RoleId);
            parameter.Add("@Status", dashboardReq.Status);
            var allObj = _unitOfWork.SP_Call.List<DashboardDetails>(SD.Proc_FBMGetDashboardDetails, parameter);
            _logger.LogInformation("GetDashboardDetails Completed");
            return allObj.ToList();
        }

        [HttpPost("ReviewerAction")]
        public async Task<IActionResult> ReviewerAction(IEnumerable<ReviewerAction> reviewerActions)
        {
            _logger.LogInformation("ReviewerAction Initiated");
            foreach (var item in reviewerActions)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@FormHeaderId", item.FormHeaderId);
                parameter.Add("@BranchId", item.BranchId);
                parameter.Add("@UserId", item.UserId);
                parameter.Add("@RoleId", item.RoleId);
                parameter.Add("@StatusId", item.StatusId);
                parameter.Add("@Comments", item.Comments);
                _unitOfWork.SP_Call.Execute(SD.Proc_FBMStatusActionClick, parameter);

                if (item.StatusId == 2)
                {
                    //var toNotifyPayroll = SendNotification(item.UserId, item.FormHeaderId, item.BranchId, item.WeekId,
                    //item.RoleId, item.StatusId, item.SubmittedBy, item.SubmitterEmail, item.CreatedBy);
                    await SendNotification(item.UserId, item.FormHeaderId, item.BranchId, item.WeekId,
                        1, item.StatusId, item.SubmittedBy, item.SubmitterEmail, item.CreatedBy);

                   // await Task.WhenAll(toNotifyPayroll, toAckSubmitter);
                    _logger.LogInformation("SendNotification Completed");
                }
                else if (item.StatusId == 3)
                {
                    await SendNotification(item.UserId, item.FormHeaderId, item.BranchId, item.WeekId,
                    1, item.StatusId, item.SubmittedBy, item.SubmitterEmail, item.CreatedBy);
                    _logger.LogInformation("SendNotification Completed");
                }
                else if (item.StatusId == 5)
                {
                    await SendNotification(item.UserId, item.FormHeaderId, item.BranchId, item.WeekId,
                    1, item.StatusId, item.SubmittedBy, item.SubmitterEmail, item.CreatedBy);
                    _logger.LogInformation("SendNotification Completed");
                }
            }
            _unitOfWork.Save();
            _logger.LogInformation("ReviewerAction Completed");
            return Ok(200);
        }

        [HttpPost("GetAuditTrailDetails")]
        public IEnumerable<AuditTrail> GetAuditTrailDetails(AuditTrail auditTrailRequest)
        {
            _logger.LogInformation("GetAuditTrailDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@FormHeaderId", auditTrailRequest.FormHeaderId);
            parameter.Add("@BranchId", auditTrailRequest.BranchId);
            var allObj = _unitOfWork.SP_Call.List<AuditTrail>(SD.Proc_FBMGetAuditTrailDetails, parameter);
            _logger.LogInformation("GetAuditTrailDetails Completed");
            return allObj.ToList();
        }

        [HttpPost("DeleteInputForm")]
        public IActionResult DeleteInputForm(IEnumerable<DeleteInputForm> inputForm)
        {
            _logger.LogInformation("DeleteInputForm Initiated");
            string formHeaderId = string.Empty;
            foreach (var item in inputForm)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@FormHeaderId", item.FormHeaderId);
                parameter.Add("@FormDetailId", item.FormDetailId);
                parameter.Add("@Option", item.Option);

                formHeaderId = item.FormHeaderId;
                _unitOfWork.SP_Call.Execute(SD.Proc_FBMDeleteInputForm, parameter);
            }

            var param = new DynamicParameters();
            param.Add("@FormHeaderId", formHeaderId);
            _unitOfWork.SP_Call.Execute(SD.Proc_FBMUpdateFormHeader, param);
            _unitOfWork.Save();
            _logger.LogInformation("DeleteInputForm Completed");
            return Ok(200);
        }


        private async Task<string> SendNotification(int userId, string formHeaderId, string branchId, string weekId, 
            int userRole, int statusId, string submittedBy, string submitterEmail, int createdBy)
        {
            try
            {
                _logger.LogInformation("SendNotification Initiated");
                string toEmail = string.Empty; string ccEmail = string.Empty;
                string userName = string.Empty; string dashboardUrl = string.Empty; string detailsUrl = string.Empty;
                string subject = string.Empty; string bodyTemplate = string.Empty;

                if (statusId == 3)
                {
                    var urlDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "baseURL");
                    var ccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "ccMail");
                    var rejectedFormSubject = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "rejectedFormSubject");
                    var loginUserDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == userId);
                    var toEmailDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == createdBy);
                    toEmail = toEmailDetails.EmailId;
                    userName = toEmailDetails.FirstName;
                    dashboardUrl = urlDetails.FBMValue + "login/" + toEmailDetails.UserId;
                    detailsUrl = urlDetails.FBMValue + "login/" + weekId + "/" + formHeaderId + "/" + toEmailDetails.UserId;

                    subject = rejectedFormSubject.FBMValue.Replace("@FormId", formHeaderId);

                    var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "rejectedFormBody");
                    bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
                        .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                        .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl)
                        .Replace("@RejectedByUserName", loginUserDetails.FirstName + ' ' + loginUserDetails.LastName);

                    return await _emailSender.SendEmailNotification(toEmail, ccMailDetails.FBMValue, subject, bodyTemplate);
                }
                else
                {
                    var urlDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "baseURL");
                    var ccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "ccMail");
                    subject = formHeaderId + "-" + "Incentive Form";
                    var loginUserDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == userId);
                    var toEmailDetails = GetNextLevelDetails(branchId, 2);
                    toEmail = toEmailDetails.EmailId;
                    userName = toEmailDetails.FirstName;

                    var creatorUserDetails = GetCreatorUserDetails(formHeaderId);


                    if (userRole == 2) // to Notify Payroll
                    {
                        dashboardUrl = urlDetails.FBMValue + "login/" + toEmailDetails.UserId;
                        detailsUrl = urlDetails.FBMValue + "login/" + weekId + "/" + formHeaderId + "/" + toEmailDetails.UserId;
                        var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toNotifyPayroll");
                        bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
                            .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                            .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl)
                            .Replace("@PrevApprover", loginUserDetails.FirstName + ' ' + loginUserDetails.LastName);
                    }
                    else if (statusId == 2) //To ACK User from RVP Approval
                    {
                        toEmail = creatorUserDetails.SubmitterEmail;
                        dashboardUrl = urlDetails.FBMValue + "login/" + creatorUserDetails.CreatedBy;
                        detailsUrl = urlDetails.FBMValue + "login/" + weekId + "/" + formHeaderId + "/" + creatorUserDetails.CreatedBy;
                        var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toAckSubmitter_RVP");
                        bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", creatorUserDetails.SubmittedBy).Replace("@WeekNo", weekId)
                            .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                            .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl);
                    }
                    else if (statusId == 5)  //To ACK User from Payroll Approval
                    {
                        toEmail = creatorUserDetails.SubmitterEmail;
                        dashboardUrl = urlDetails.FBMValue + "login/" + creatorUserDetails.CreatedBy;
                        detailsUrl = urlDetails.FBMValue + "login/" + weekId + "/" + formHeaderId + "/" + creatorUserDetails.CreatedBy;
                        var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toAckSubmitter_Payroll");
                        bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", creatorUserDetails.SubmittedBy).Replace("@WeekNo", weekId)
                            .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                            .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl);
                    }

                    return await _emailSender.SendEmailNotification(toEmail, ccMailDetails.FBMValue, subject, bodyTemplate);
                }
            }
            catch (Exception ex)
            {
                return "200";
            }
            
        }

        public FBMUsers GetNextLevelDetails(string branchId, int level)
        {
            var userIdDetails = _unitOfWork.FBMApprover.GetFirstOrDefault(x => x.BranchId == branchId);

            if (level == 1)
            {
                var nextLevelUserDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == userIdDetails.PrimaryApproverUserId);
                return nextLevelUserDetails;
            }
            else {
                var nextLevelUserDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == userIdDetails.PayrollApproverUserId);
                return nextLevelUserDetails;
            }
        }

        public CreatorUserDetails GetCreatorUserDetails(string formHeaderId)
        {
            CreatorUserDetails creatorObj = new CreatorUserDetails();
            var formHeaderDetails = _unitOfWork.FormHeader.GetFirstOrDefault(x => x.FormHeaderId == formHeaderId);
            var userDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == formHeaderDetails.CreatedBy);
            creatorObj.CreatedBy = Convert.ToInt32(formHeaderDetails.CreatedBy);
            creatorObj.SubmittedBy = userDetails.FirstName + ' ' + userDetails.LastName;
            creatorObj.SubmitterEmail = userDetails.EmailId;
            return creatorObj;
        }

        [HttpPost("GetPayrollPendingDetails")]
        public IEnumerable<PayrollPending> GetPayrollPendingDetails(PayrollPending payrollPending)
        {
            _logger.LogInformation("GetPayrollPendingDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@StatusId", payrollPending.StatusId);
            parameter.Add("@Option", payrollPending.Option);
            parameter.Add("@BranchId", payrollPending.BranchId);
            parameter.Add("@WeekId", payrollPending.WeekId);
            parameter.Add("@PayGroup", payrollPending.PayGroupCode);

            var allObj = _unitOfWork.SP_Call.List<PayrollPending>(SD.Proc_FBMGetEmployeeDetailsPendingByPayroll, parameter);
            _logger.LogInformation("GetPayrollPendingDetails Completed");
            return allObj.ToList();
        }
    }
}
