﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FortyOneChatServices.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastTimeOnline { get; set; }
    }
}