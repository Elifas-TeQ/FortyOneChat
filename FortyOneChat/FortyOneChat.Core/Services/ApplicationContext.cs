using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services
{
    public class ApplicationContext : IApplicationContext
    {
        public User CurrentUser { get; set; }
        //public string ServiceUri => @"http://192.168.127.2:7777/api/Chat";
        public string ServiceUri => @"http://10.129.68.199:8888/api/Chat";
    }
}
