using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class FBMConfiguration
    {
        [Key]
        public int Id { get; set; }

        public string FBMKey { get; set; }

        public string FBMValue { get; set; }
    }
}
