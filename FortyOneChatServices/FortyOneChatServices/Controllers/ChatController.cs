using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FortyOneChatServices.Models;


namespace FortyOneChatServices.Controllers
{
    [RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        public static List<User> Users = new List<User>();
        public static List< Message> Messages = new List<Message>();
        private const int DELTA_ONLINE = 3;

        [HttpGet]
        [Route("History")]
        public IEnumerable<Message> GetChatHistory(int UserId)
        {
            var curUser = Users.Single(u => u.Id == UserId);
            if (curUser!= null)
            {
                curUser.LastTimeOnline = DateTime.Now.AddMinutes(DELTA_ONLINE);
            }
            
            return Messages; 
        }

        [HttpPost]
        [Route("Login")]
        public User Login(string userName)
        {
            if (Users.Count == 0)
            {
                var curUser = new User() { Id = 0, Name = userName, LastTimeOnline = DateTime.Now };
                Users.Add(curUser);
                return curUser;
            }
            else
            {
               var curUser =  Users.Single(u => u.Name.Equals(userName));
               return curUser!= null ? curUser : new User() { Id = 0, Name = userName, LastTimeOnline = DateTime.Now };   
            }
        }
        
        [HttpPost]
        [Route("SendMessage")]
        public bool SendMessage(Message message)
        {
            message.Id = Messages.Count != 0 ? Messages.Max(x => x.Id) + 1 : 0;
            Messages.Add(message);
        
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
