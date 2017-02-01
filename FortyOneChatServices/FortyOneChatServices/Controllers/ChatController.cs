using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FortyOneChatServices.Models;
using FortyOneChatServices.Models.Requests;

namespace FortyOneChatServices.Controllers
{
    [RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        public static Dictionary<int, string> Users = new Dictionary<int, string>();
        public static Dictionary<int, Message> Messages = new Dictionary<int, Message>();

        [HttpGet]
        [Route("History")]
        public IEnumerable<Message> GetHistory()
        {
            return Messages.Values.Take<Message>(20); ;
        }

        [HttpPost]
        [Route("Login")]
        public bool Login(string userName)
        {
            bool res = Users.Values.Contains<string>(userName);
            if (!res)
            {
                Users.Add(Users.Keys.LastOrDefault<int>()+1,userName);
            }

            return !res;
        }
        
        [HttpPost]
        [Route("SendMessage")]
        public bool SendMessage(Message message)
        {
            var index = Messages.Count + 1;
            Messages.Add(index, message);
           
            return true;
        }

        [HttpGet]
        [Route("UserStatus")]
        public bool  GetUserStatus()
        {
            return true;
        }
        
    }
}
