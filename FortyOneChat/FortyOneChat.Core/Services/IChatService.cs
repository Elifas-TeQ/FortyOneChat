using System.Collections.Generic;
using FortyOneChat.Core.Models;
using System.Threading.Tasks;

namespace FortyOneChat.Core.Services
{
    public interface IChatService
    {
		Task<List<Message>> GetChatHistory();
		Task<bool> SendMessage(Message message);
    }
}