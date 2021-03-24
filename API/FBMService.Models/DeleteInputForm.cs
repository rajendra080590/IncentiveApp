using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class DeleteInputForm
    {
        public string FormHeaderId { get; set; }
        public string FormDetailId { get; set; }

        public int? Option { get; set; }
    }
}
