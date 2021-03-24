using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using FBMICService.DataAccess.Repository.IRepository;
using FBMICService.Models;
using FBMICService.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FBMICService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewFormController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<NewFormController> _logger;
        const string FILE_PATH = @"D:\SampleFileUpload\";

        public NewFormController(IUnitOfWork unitOfWork, IConfiguration configuration, IEmailSender emailSender, ILogger<NewFormController> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost("GetNewFormDetails")]
        public  IActionResult GetNewFormDetails(InputForm inputForm)
       {
            _logger.LogInformation("GetNewFormDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@BranchId", inputForm.BranchId);
            parameter.Add("@WeekId", inputForm.WeekId);
            parameter.Add("@FormHeaderId", inputForm.FormHeaderId);
            //var allObj = _unitOfWork.CoverType.GetAll();
            var allObj =  _unitOfWork.SP_Call.List<InputForm>(SD.Proc_FBMGetInputFormDetails, parameter);
            _logger.LogInformation("GetNewFormDetails Completed");
            return Ok(allObj);
        }

        [HttpPost("GetNewFormDetails/{BranchId}/{WeekId}/{FormHeaderId}")]
        public IActionResult GetNewFormDetails1(string BranchId, string WeekId, string FormHeaderId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@BranchId", BranchId);
            parameter.Add("@WeekId", WeekId);
            parameter.Add("@FormHeaderId", FormHeaderId);
            //var allObj = _unitOfWork.CoverType.GetAll();
            var allObj = _unitOfWork.SP_Call.List<InputForm>(SD.Proc_FBMGetInputFormDetails, parameter);

            return Ok(allObj);
        }

        
        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert(IEnumerable<InputForm> inputForm)
        {
           //[FromBody] FileToUpload theFile
            _logger.LogInformation("Upsert New Form Initiated");
            

            var firstItem = inputForm.First();
            var param = new DynamicParameters();
            param.Add("@WeekId", firstItem.WeekId);
            param.Add("@BranchId", firstItem.BranchId);
            param.Add("@Status", firstItem.StatusId);
            param.Add("@UserId", firstItem.UserId);
            param.Add("@FormHeaderId", firstItem.FormHeaderId);
            double totalAmount = inputForm.Sum(v => Convert.ToDouble(v.IncentiveAmount));
            param.Add("@Incentivised", inputForm.Count());
            param.Add("@TotalAmount", totalAmount);
            param.Add("@IncentiveWeekDate", firstItem.IncentiveWeekDate);
            param.Add("@Comments", firstItem.FormHeaderComments);
            param.Add("@FileName", firstItem.Attachment);
            param.Add("@RoleId", firstItem.RoleId);

            var result =_unitOfWork.SP_Call.List<FormHeader>(SD.Proc_FBMInsertUpdateFormHeader, param);
            string formHeaderId = string.Empty;
            string toEmail = string.Empty;
            int approverId = 0;
            dynamic toEmailDetails = null;
            foreach (var res in result)
            {
                formHeaderId = res.FormHeaderId;
                toEmailDetails = _unitOfWork.Login.GetFirstOrDefault(u => u.UserId == res.ApproverId);
                toEmail = toEmailDetails.EmailId;
                approverId = Convert.ToInt32(res.ApproverId);
            }

            if (result.Count() > 0)
            {
                //var forHeaderId = result[0].FormHeaderId;
                foreach (var item in inputForm)
                {
                    var parameter = new DynamicParameters();
                    parameter.Add("@WeekId", item.WeekId);
                    parameter.Add("@BranchId", item.BranchId);
                    parameter.Add("@Status", item.StatusId);
                    parameter.Add("@EmployeeId", item.EmployeeId);
                    parameter.Add("@IncentiveAmount", item.IncentiveAmount);
                    parameter.Add("@UserId", item.UserId);
                    parameter.Add("@FormHeaderId", formHeaderId);
                    parameter.Add("@FormDetailsId", item.FormDetailId);
                    parameter.Add("@Comments", item.Comments);
                    parameter.Add("@ToDelete", item.ToDelete);

                    _unitOfWork.SP_Call.Execute(SD.Proc_FBMInsertUpdateInputFormDetails, parameter);

                }
                var updateParam = new DynamicParameters();
                updateParam.Add("@FormHeaderId", formHeaderId);
                _unitOfWork.SP_Call.Execute(SD.Proc_FBMUpdateFormHeader, updateParam);
                _unitOfWork.Save();
            }
            //await _emailSender.SendEmailNotification("", "", "");
            //await SendNotification(firstItem.UserId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 1, 1);

            dynamic submitter, approver;
            if (firstItem.RoleId == 2)
            {
                await SendNotification(firstItem.UserId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 2, firstItem.StatusId, true, firstItem.UserId); 
                //approver = SendNotification(approverId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 3, firstItem.StatusId, true, firstItem.UserId); 
            }
            else {
                submitter = SendNotification(firstItem.UserId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 1, firstItem.StatusId); 
                approver = SendNotification(approverId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 2, firstItem.StatusId);
                await Task.WhenAll(submitter, approver);
            }
            _logger.LogInformation("Send Notification Completed");
            _logger.LogInformation("Upsert New Form Completed");
            return Ok(result);
        }

        [HttpPost("DraftUpsert")]
        public async Task<IActionResult> DraftUpsert(IEnumerable<InputForm> inputForm)
        {
            _logger.LogInformation("Draft Upsert Initiated");
            var firstItem = inputForm.First();
            var param = new DynamicParameters();
            param.Add("@WeekId", firstItem.WeekId);
            param.Add("@BranchId", firstItem.BranchId);
            param.Add("@Status", firstItem.StatusId);
            param.Add("@UserId", firstItem.UserId);
            param.Add("@FormHeaderId", firstItem.FormHeaderId);
            double totalAmount = inputForm.Sum(v => Convert.ToDouble(v.IncentiveAmount));
            param.Add("@Incentivised", inputForm.Count());
            param.Add("@TotalAmount", totalAmount);
            param.Add("@IncentiveWeekDate", firstItem.IncentiveWeekDate);

            var result = _unitOfWork.SP_Call.List<FormHeader>(SD.Proc_FBMInsertUpdateDraftInputFormHeader, param);
            string formHeaderId = string.Empty;
            string toEmail = string.Empty;
            dynamic toEmailDetails = null;
            foreach (var res in result)
            {
                formHeaderId = res.FormHeaderId;
                toEmailDetails = _unitOfWork.Login.GetFirstOrDefault(u => u.UserId == res.ApproverId);
                toEmail = toEmailDetails.EmailId;
            }

            if (result.Count() > 0)
            {
                //var forHeaderId = result[0].FormHeaderId;
                foreach (var item in inputForm)
                {
                    var parameter = new DynamicParameters();
                    parameter.Add("@WeekId", item.WeekId);
                    parameter.Add("@BranchId", item.BranchId);
                    parameter.Add("@Status", item.StatusId);
                    parameter.Add("@EmployeeId", item.EmployeeId);
                    parameter.Add("@IncentiveAmount", item.IncentiveAmount);
                    parameter.Add("@UserId", item.UserId);
                    parameter.Add("@FormHeaderId", formHeaderId);
                    parameter.Add("@FormDetailsId", item.FormDetailId);
                    parameter.Add("@Comments", item.Comments);

                    _unitOfWork.SP_Call.Execute(SD.Proc_FBMInsertUpdateInputFormDetails, parameter);

                }
                _unitOfWork.Save();
            }
            //await _emailSender.SendEmailNotification("", "", "");
            //await SendNotification(firstItem.UserId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 1, firstItem.StatusId);
            //await SendNotification(firstItem.ApproverId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 2, firstItem.StatusId);

            var submitter = SendNotification(firstItem.UserId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 1, firstItem.StatusId); ;
            var approver = SendNotification(firstItem.ApproverId, formHeaderId, firstItem.BranchId, firstItem.WeekId, 2, firstItem.StatusId); ;
            await Task.WhenAll(submitter, approver);

            _logger.LogInformation("Send Notification Completed");
            _logger.LogInformation("Draft Upsert Completed");
            return Ok(result);
        }

        private async Task<string> SendNotification(int userId, string formHeaderId, string branchId, string weekId, int userRole, int statusId,[Optional] bool isBMAsSubmiiter, [Optional] int loginUserId)
        {
            try
            {
                _logger.LogInformation("Send Notification Initiated");
                string toEmail = string.Empty; string ccEmail = string.Empty;
                string userName = string.Empty; string dashboardUrl = string.Empty; string detailsUrl = string.Empty;
                string subject = string.Empty; string bodyTemplate = string.Empty;
                if (statusId == 4)
                {
                    return "ok";
                }
                else
                {
                    var urlDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "baseURL");
                    var ccMailDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "ccMail");
                    subject = formHeaderId + "-" + "Incentive Form";

                    var toEmailDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == userId);
                    toEmail = toEmailDetails.EmailId;
                    userName = toEmailDetails.FirstName;
                    dashboardUrl = urlDetails.FBMValue + "login";
                    detailsUrl = urlDetails.FBMValue + "login/" + weekId + "/" + formHeaderId + "/" + userId;

                    if (isBMAsSubmiiter == true)
                    {
                        var loginUserDetails = _unitOfWork.Login.GetFirstOrDefault(x => x.UserId == loginUserId);
                        if (userRole == 2)
                        {
                            var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toNotifyBMAsSubmitter");
                            bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
                                .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                                .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl);
                        }
                        else if (userRole == 3)
                        {
                            var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toNotifyPayroll");
                            bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
                                .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                                .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl)
                                .Replace("@PrevApprover", loginUserDetails.FirstName + ' ' + loginUserDetails.LastName);
                        }
                    }
                    else
                    {
                        if (userRole == 1)
                        {
                            var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toNotifySubmitter");
                            bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
                                .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                                .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl);
                        }
                        else if (userRole == 2)
                        {
                            var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toNotifyRVP");
                            bodyTemplate = bodyTemplateDetails.FBMValue.Replace("@UserName", userName).Replace("@WeekNo", weekId)
                                .Replace("@BranchNo", branchId).Replace("@FormHeaderId", formHeaderId)
                                .Replace("@DashboardUrl", dashboardUrl).Replace("@DetailsUrl", detailsUrl);
                        }
                    }


                    return await _emailSender.SendEmailNotification(toEmail, ccMailDetails.FBMValue, subject, bodyTemplate);
                }
            }
            catch (Exception ex)
            {
                return "ok";
            }
            
        }

        private async Task<string> SendNotification1(string toMail, string ccMail) 
        {
            var client = new HttpClient();

            // requires using System.Text.Json;
            var bodyTemplateDetails = _unitOfWork.FBMConfiguration.GetFirstOrDefault(x => x.FBMKey == "toNotifySubmitter");
            var jsonData = JsonSerializer.Serialize(new
            {
                toemail = "rajendra.patil@fbmsales.com",
                ccemail = "rajendra1.patil@birlasoft.com",
                task = "FBM Email Notification!",
                body = bodyTemplateDetails.FBMValue
            });

            HttpResponseMessage result = await client.PostAsync(_configuration["LogicAppSendEmailUrl"],
                new StringContent(jsonData, Encoding.UTF8, "application/json"));

            var statusCode = result.StatusCode.ToString();
            return statusCode;
        }

        [HttpPost("GetPayrollDateDetails")]
        public IActionResult GetPayrollDateDetails(PayrollDateRequest payrollDateRequest)
        {
            _logger.LogInformation("GetPayrollDateDetails Initiated");
            var parameter = new DynamicParameters();
            parameter.Add("@CurrentDate", payrollDateRequest.CurrentDate);
            var allObj = _unitOfWork.SP_Call.OneRecord<IncentiveAppCalendar>(SD.Proc_FBMGetPayrollDateDetails, parameter);
            _logger.LogInformation("GetPayrollDateDetails Completed");
            return Ok(allObj);
        }

        [HttpPost("UploadFiles")]
        public IActionResult UploadFiles([FromBody] FileToUpload theFile)
        {
            var filePathName = FILE_PATH + Path.GetFileNameWithoutExtension(theFile.FileName) + "-" +
            DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
            Path.GetExtension(theFile.FileName);

            if (theFile.FileAsBase64.Contains(","))
            {
                theFile.FileAsBase64 = theFile.FileAsBase64.Substring(theFile.FileAsBase64.IndexOf(",") + 1);
            }
            theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);
            using (var fs = new FileStream(filePathName, FileMode.CreateNew))
            {
                fs.Write(theFile.FileAsByteArray, 0, theFile.FileAsByteArray.Length);
            }
            return Ok();
        }


    }


}
