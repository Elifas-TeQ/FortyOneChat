using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Core.Services
{
    public interface IChatService
    {
        Task<IList<Message>> GetMessages();
    }
}
