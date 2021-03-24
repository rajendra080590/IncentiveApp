using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models.ViewModels
{
    public class InputFormVM
    {
        public FormHeader formHeader { get; set; }

        public FormDetails formDetails { get; set; }

        public FormHeaderDetails formHeaderDetails { get; set; }
    }
}
