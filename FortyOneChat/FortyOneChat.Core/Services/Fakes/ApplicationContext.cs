using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Core.Services.Fakes
{
    public class ApplicationContext : IApplicationContext
    {
        private User FakedUser;
        public ApplicationContext()
        {
            FakedUser = new User { Id = 1, Name = "Vasya" };
        }
        public User CurrentUser
        {
            get
            {
                return FakedUser;
            }
        }
    }
}
