﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    public class ModelUser
    {
        public string full_name { get; set; }
        public string phone_number { get; set; }
        public string profile_picture { get; set; }
    }

    public class ItemUser
    {
        public ModelUser user { get; set; }
    }

}
