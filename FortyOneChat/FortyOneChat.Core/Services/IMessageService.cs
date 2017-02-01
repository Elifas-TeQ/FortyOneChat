using System.Threading.Tasks;

namespace FortyOneChat.Core
{
	public interface IMessageService
	{
		Task<MessageResponse> GetNewMessages(int lastMessageId);
		Task<bool> SendNewMessage(Message message);
	}
}
