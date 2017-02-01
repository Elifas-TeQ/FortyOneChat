using System.Collections.Generic;
using System.Threading.Tasks;

namespace FortyOneChat.Core.Services
{
	public interface IUserService
	{
		Task<List<string>> GetOnlineUsers();
		Task<int> LogIn(string userName);
	}
}