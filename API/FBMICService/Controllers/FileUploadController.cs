using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using FBMICService.Models;
using FBMICService.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http;
using FBMICService.Helpers;
using FBMICService.Enums;

namespace FBMICService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        //const string FILE_PATH = @"D:\Birlasoft\ProjectDetails\FBM-Sales-Incentive-Calculation-Service\FBMICService\UploadedFiles\";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        //const string FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", );

        public FileUploadController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        public IActionResult Post([FromBody] FileToUpload theFile)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var FILE_PATH = Path.Combine(webRootPath, @"UploadedFiles\");
            var filePathName = FILE_PATH + theFile.UpdatedFileName + "_" +
                Path.GetFileNameWithoutExtension(theFile.FileName) +
                Path.GetExtension(theFile.FileName);

            // Remove file type from base64 encoding, if any
            if (theFile.FileAsBase64.Contains(","))
            {
                theFile.FileAsBase64 = theFile.FileAsBase64
                  .Substring(theFile.FileAsBase64.IndexOf(",") + 1);
            }

            // Convert base64 encoded string to binary
            theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);

            //convert bytes to memory stream
            //var contents = new StreamContent(new MemoryStream(theFile.FileAsByteArray));
            Stream stream = new MemoryStream(theFile.FileAsByteArray);

            string resFileName = Path.GetFileName(theFile.FileName);
            // Upload file to blob storage
            //AzureStorage.UploadFileAsync(ContainerType.incentiveappfiles, resFileName, stream);

            // Write binary file to server path
            using (var fs = new FileStream(filePathName, FileMode.Create))
            {
                fs.Write(theFile.FileAsByteArray, 0, theFile.FileAsByteArray.Length);
            }

            return Ok();
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, "UploadedFiles");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.OpenReadStream().Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, "UploadedFiles");
            var filePath = Path.Combine(uploads, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), file);
        }

        [HttpGet]
        [Route("files/{formHeaderId}")]
        public IActionResult Files(string formHeaderId)
        {
            //var result = new List<FileDetails>();
            List<FileDetails> fileDetails = new List<FileDetails>();
            //string formHeaderId = "MI-002-21_W04-03";
            var resSaveFiles = _unitOfWork.FormHeader.GetFirstOrDefault(u => u.FormHeaderId == formHeaderId);
            if (resSaveFiles != null)
            {
                fileDetails.Add(new FileDetails { FormHeaderComments = resSaveFiles.Comments });
                string saveFile = resSaveFiles.FileName;
                if (!string.IsNullOrEmpty(saveFile))
                {
                    string[] saveFileList = saveFile.Split(",");

                    var uploads = Path.Combine(_hostEnvironment.WebRootPath, "UploadedFiles");
                    if (Directory.Exists(uploads))
                    {
                        var provider = _hostEnvironment.ContentRootFileProvider;
                        foreach (string fileName in Directory.GetFiles(uploads))
                        {
                            var fileInfo = provider.GetFileInfo(fileName);
                            var filename1 = Path.GetFileName(fileName);
                            //result.Add(fileInfo.Name);
                            foreach (string file in saveFileList)
                            {
                                if (filename1.Trim() == file.Trim())
                                {
                                    // result.Add(filename1);
                                    fileDetails.Add(new FileDetails { FileName = filename1 });
                                }
                            }
                        }
                    }
                }
                
            }
            return Ok(fileDetails.ToList());
        }


        [HttpGet]
        [Route("GetBlobFiles")]
        public IActionResult GetBlobFiles(string formHeaderId)
        {
            //var result = new List<FileDetails>();
            List<FileDetails> fileDetails = new List<FileDetails>();
            //string formHeaderId = "MI-002-21_W04-03";
            var resSaveFiles = _unitOfWork.FormHeader.GetFirstOrDefault(u => u.FormHeaderId == formHeaderId);
            if (resSaveFiles != null)
            {
                fileDetails.Add(new FileDetails { FormHeaderComments = resSaveFiles.Comments });
                string saveFile = resSaveFiles.FileName;
                if (!string.IsNullOrEmpty(saveFile))
                {
                    string[] saveFileList = saveFile.Split(",");

                    var filResList = AzureStorage.GetFilesListAsync(ContainerType.incentiveappfiles);
                }

            }
            return Ok(fileDetails.ToList());
        }


        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
