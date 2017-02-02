using System.Collections.Generic;
using System.Threading.Tasks;
using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services
{
	public interface IUserService
	{
		Task<List<string>> GetOnlineUsers();
		Task<User> LogIn(string userName);
	}
}