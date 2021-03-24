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
    public class ReminderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ReminderController> _logger;
        public ReminderController(IUnitOfWork unitOfWork, ILogger<ReminderController> logger, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpGet("ReminderCall")]
        public async Task<IActionResult> ReminderCall()
        {
            _logger.LogInformation("ReminderCall Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@WeekId", null);
            var allObj = _unitOfWork.SP_Call.List<ReminderDetails>(SD.Proc_FBMGetPreparerListForNotify, parameter);
            foreach (var item in allObj)
            {
                await SendNotification(item.PreparerEmailId, null, item.EmailBody);
            }
            _logger.LogInformation("ReminderCall Completed");
            return Ok(200);
        }


        [HttpGet("GetNotApprovedFormList")]// Used to Notify Secondary Approver
        public async Task<IActionResult> GetNotApprovedFormList()
        {
            _logger.LogInformation("GetNotApprovedFormList Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@WeekId", null);
            var allObj = _unitOfWork.SP_Call.List<NotApprovedFormDetails>(SD.Proc_FBMGetNotApprovedFormList, parameter);
            foreach (var item in allObj)
            {
                var updateParam = new DynamicParameters();
                updateParam.Add("@WeekId", item.WeekId);
                //_unitOfWork.SP_Call.Execute(SD.Proc_FBMUpdateApproverDetails, updateParam);

                await ToNotifySecondaryApprover(item.SecondaryApproverEmailId, item.FormHeaderId, item.WeekId, item.BranchId, item.SecondaryApproverUserId);
            }
            //_unitOfWork.Save();
            _logger.LogInformation("GetNotApprovedFormList Completed");
            return Ok(200);
        }

        [HttpGet("ToNotifyAllUsers")] //To send reminder on Monday and Friday
        public async Task<IActionResult> ToNotifyAllUsers()
        {
            _logger.LogInformation("ToNotifyAllUsers Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@WeekId", null);
            var allObj = _unitOfWork.SP_Call.List<NotifyUserDetails>(SD.Proc_FBMGetUserListForNotify, parameter);
            foreach (var item in allObj)
            {
                await ToNotifyUser(item.EmailId, item.BranchId, item.UserId, item.UserType, item.UserName, item.WeekId );
            }
            _logger.LogInformation("ToNotifyAllUsers Completed");
            return Ok(200);
        }

        //Second Monday Reminder
        [HttpGet("GetNotSubmittedFormUserList")]// Used to Notify User who has not subitted form of that week for branch
        public async Task<IActionResult> GetNotSubmittedFormUserList()
        {
            _logger.LogInformation("GetNotSubmittedFormUserList Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@WeekId", null);
            var allObj = _unitOfWork.SP_Call.List<NotifyUserDetails>(SD.Proc_FBMGetNotSubmitIncentiveFormUserList, parameter);
            foreach (var item in allObj)
            {
                await ToNotifyUsersNotsubmittedForm(item.EmailId, item.WeekId, item.BranchId);
            }
            _logger.LogInformation("GetNotSubmittedFormUserList Completed");
            return Ok(200);
        }

        private async Task<string> SendNotification(string toEmail, string fromEmail, string emailBody)
        {
            var ccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "ccMail");
            var preparerMailSubject = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "preparerEmailSubject");
            return await _emailSender.SendEmailNotification(toEmail, ccMailDetails.FBMValue, preparerMailSubject.FBMValue, emailBody);
        }

        private async Task<string> ToNotifySecondaryApprover(string toEmail, string formHeaderId, string weekId, string branchId, int userId)
        {
            _logger.LogInformation("To Notify Secondary Approver Initiated");
            string userName = string.Empty; string dashboardUrl = string.Empty; string detailsUrl = string.Empty;
            string subject = string.Empty; string bodyTemplate = string.Empty;
            var ccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "ccMail");
            var toEmailDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == userId);
            userName = toEmailDetails.FirstName;
            var urlDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "baseURL");
            subject = formHeaderId + "-" + "Incentive Form";
            dashboardUrl = urlDetails.FBMValue + "login";
            detailsUrl = urlDetails.FBMValue + "login/" + weekId + "/" + formHeaderId + "/" + userId;

            var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toNotifySecondaryApprover");
            bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
                .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl);

            return await _emailSender.SendEmailNotification(toEmail, ccMailDetails.FBMValue, subject, bodyTemplate);
        }

        private async Task<string> ToNotifyUser(string toEmail, string branchId, int userId, string userType, string userName, string weekId)
        {
            _logger.LogInformation("To Notify User Initiated");
            string dashboardUrl = string.Empty; string detailsUrl = string.Empty;
            string subject = string.Empty; string bodyTemplate = string.Empty;
            var ccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "ccMail");
            var bccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "bccMail");
            var urlDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "baseURL");
            dynamic subjectDetails, bodyTemplateDetails;
            DayOfWeek wkDay = DateTime.Today.DayOfWeek;
            if (Convert.ToString(wkDay).ToUpper() == "MONDAY")
            {
                subjectDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toFinishProcessSubject");
                bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "mondayReminderToFinishProcess");
            }
            else {
                subjectDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toStartProcessSubject");
                bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "fridayReminderToStartTheProcess");
            }
            
            subject = subjectDetails.FBMValue.Replace("@WeekNo", weekId);
            dashboardUrl = urlDetails.FBMValue + "login";
            //detailsUrl = urlDetails.FBMValue + "login/" + weekId + "/" + formHeaderId + "/" + userId;
            bodyTemplate = bodyTemplateDetails.FBMValue;
            bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
               .Replace("@DashboardUrl", dashboardUrl);

            return await _emailSender.SendEmailNotification(toEmail, ccMailDetails.FBMValue, subject, bodyTemplate, bccMailDetails.FBMValue);
        }

        private async Task<string> ToNotifyUsersNotsubmittedForm(string toEmail, string weekId, string branchId)
        {
            _logger.LogInformation("To Notify Users Submitted Form  Reminder Initiated");
            string userName = string.Empty; string dashboardUrl = string.Empty; string detailsUrl = string.Empty;
            string subject = string.Empty; string bodyTemplate = string.Empty;
            var ccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "ccMail");
            var urlDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "baseURL");

            var subjectDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "subjectNotSubmittedFormUserToNotify");
            subject = subjectDetails.FBMValue.Replace("@WeekNo", weekId).Replace("@BranchId", branchId);
            dashboardUrl = urlDetails.FBMValue + "login";

            var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "bodyNotSubmittedFormUserToNotify");
            bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId).Replace("@BranchId", branchId)
                .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl);

            return await _emailSender.SendEmailNotification(toEmail, ccMailDetails.FBMValue, subject, bodyTemplate);
        }
    }
}
