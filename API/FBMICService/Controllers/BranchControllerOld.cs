using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBMICService.Data;
using FBMICService.Data.Repo;
using FBMICService.Interfaces;
using FBMICService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FBMICService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchControllerOld : ControllerBase
    {
        //private readonly DataContext _dc;
        //private readonly IBranchRepository repo;
        private readonly IUnitOfWork2 uow;

        //public BranchController(DataContext dc, IBranchRepository repo) 
        //{
        //    this._dc = dc;
        //    this.repo = repo;
        //}
        public BranchControllerOld(IUnitOfWork2 uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var branches = await repo.GetBranchesAsync();
            var branches = await uow.BranchRepository.GetBranchesAsync();
            return Ok(branches);
            //return new string[] { "Canada", "Alaska" };
        }


        //post api/branch/add?BranchName=ST;totalEmployee=10;status=1
        [HttpPost("Add")]
        [HttpPost("post")]
        [HttpPost("Add/{BranchName}")]
        //public async Task<IActionResult> AddBranch(string BranchName, int totalEmployee, bool status)
        public async Task<IActionResult> AddBranch(Branch branch)
        {
            //Branch branch = new Branch();
            //branch.BranchName = BranchName;
            //branch.TotalEmployee = totalEmployee;
            //branch.Status = status;
            //await _dc.Branches.AddAsync(branch);
            //await _dc.SaveChangesAsync();

            //repo.AddBranch(branch);
            //await repo.SaveAsync();

            uow.BranchRepository.AddBranch(branch);
            await uow.SaveAync();
            return Ok(branch);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            //var branch = await _dc.Branches.FindAsync(id);
            //_dc.Branches.Remove(branch);
            //await _dc.SaveChangesAsync();

            //repo.deleteBranch(id);
            //await repo.SaveAsync();

            uow.BranchRepository.deleteBranch(id);
            await uow.SaveAync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "New York";
        }
    }
}
