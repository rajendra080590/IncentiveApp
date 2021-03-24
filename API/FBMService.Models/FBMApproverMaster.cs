using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FBMICService.Models
{
    public class FBMApproverMaster
    {
        [Key]
        public int Id { get; set; }

        public int RegionCode { get; set; }

        public string Region { get; set; }

        public string BranchId { get; set; }

        public int PreparerUserId { get; set; }

        public string PreparerName { get; set; }

        public string PreparerEmailId { get; set; }


        public int? PrimaryApproverUserId { get; set; }

        public string PrimaryApproverName { get; set; }

        public string PrimaryApproverEmailId { get; set; }

        public int? SecondaryApproverUserId { get; set; }

        public string SecondaryApproverName { get; set; }

        public string SecondaryApproverEmailId { get; set; }

        public int? PayrollApproverUserId { get; set; }

        public string PayrollApproverName { get; set; }

        public string PayrollApproverEmailId { get; set; }

    }
}
