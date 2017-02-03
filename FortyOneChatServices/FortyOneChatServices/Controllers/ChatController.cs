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
        private const int DELTA_ONLINE = 2;

        [HttpGet]
        [Route("History")]
        public IEnumerable<Message> GetChatHistory(int UserId)
        {
            var curUser = Users.SingleOrDefault(u => u.Id == UserId);
            if (curUser!= null)
            {
                curUser.LastTimeOnline = DateTime.UtcNow.AddMinutes(DELTA_ONLINE);

                foreach (var message in Messages.Where(m => m.Author.Id == curUser.Id))
                {
                    message.Author.LastTimeOnline = DateTime.UtcNow.AddMinutes(DELTA_ONLINE);
                }
            }
            
            return Messages; 
        }

        [HttpPost]
        [Route("Login")]
        public User Login(string userName)
        {
            User retUser;
             if (Users.Count == 0)
            {
                var curUser = new User() { Id = 0, Name = userName };
                Users.Add(curUser);
                retUser = curUser;
            }
            else
            {
               var curUser =  Users.SingleOrDefault(u => u.Name.Equals(userName));
                if (curUser == null)
                {
                    var newUser = new User() { Id = Users.Max(x => x.Id) + 1, Name = userName };
                    Users.Add(newUser);
                    retUser = newUser;
                }
                else
                {
                    retUser =  curUser;
                }
            }

            retUser.LastTimeOnline = DateTime.UtcNow.AddMinutes(DELTA_ONLINE);
            Messages.Add(new Message() { Id = -1, Author = retUser});

            return retUser;
        }
        
        [HttpPost]
        [Route("SendMessage")]
        public int SendMessage(Message message)
        {
            message.Id = Messages.Count != 0 ? Messages.Max(x => x.Id) + 1 : 0;
            message.Author.LastTimeOnline = DateTime.UtcNow.AddMinutes(DELTA_ONLINE);
            Messages.Add(message);
        
            return message.Id;
        }

        [HttpGet]
        [Route("UserStatus")]
        public bool  GetUserStatus()
        {
            return true;
        }
        
        [HttpGet]
        [Route("ClearAllMessages")]
        public void ClearAllMessages()
        {
            Messages.Clear();
        }
    }
}
