﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class CreateBranchRequest
    {
        public string Branch { get; set; }
        public string Ref { get; set; }
    }
}
