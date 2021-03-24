using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class BranchMaster
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SXId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string BranchId { get; set; }

        public string Division { get; set; }

        public string Type { get; set; }

        [Required]
        [MaxLength(300)]
        public string BranchName { get; set; }

        [MaxLength(500)]
        public string Addess { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string StProv { get; set; }

        public string ZipPostal { get; set; }

        public string BranchManager { get; set; }

        public string Time { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string TollFree { get; set; }

        public string HoursOfOperation { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public string FBMDate { get; set; }

        public string DateOpen { get; set; }

        public string DateClose { get; set; }

        public string RegionCode { get; set; }

        public string Region { get; set; }

        public string MetroDistrict { get; set; }

        public string Category { get; set; }

        public string RegionalVP { get; set; }

        public string DistrictManager { get; set; }

        public string OprtaionalManager { get; set; }

        public string OtherKeyContact { get; set; }

        public string KelmarContact { get; set; }

        public string DSIContact { get; set; }

        public string E_19Link { get; set; }

        public string SafetyCordinator { get; set; }

        public string HRGeneralist { get; set; }

        public string ADPCo { get; set; }

        public string SubsidiaryName { get; set; }

        public string Asset_Stock { get; set; }

        public string Legacy { get; set; }

        public string Notes { get; set; }

        public string UpdateFix { get; set; }

    }
}
