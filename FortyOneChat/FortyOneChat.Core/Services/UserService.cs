using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IApplicationContext _applicationContext;

		public UserService(IApplicationContext applicationContext)
		{
			this._applicationContext = applicationContext;
		}

		public async Task<List<string>> GetAllUser()
		{
			var httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.GetAsync($"{this._applicationContext.ServiceUri}/AllUsers").ConfigureAwait(false);
			return await this.GetListNamesFromHttpResponseMessage(response);
		}

		public async Task<List<string>> GetOnlineUsers()
		{
			var httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.GetAsync($"{this._applicationContext.ServiceUri}/OnlineUsers").ConfigureAwait(false);
			return await this.GetListNamesFromHttpResponseMessage(response);
		}

		public async Task<User> LogIn(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				throw new ArgumentNullException(nameof(userName));
			}
			string content = JsonConvert.SerializeObject(userName);
			var stringContent = new StringContent(content);
			var httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.PostAsync($"{this._applicationContext.ServiceUri}/Login?userName={userName}", stringContent)
			                                               .ConfigureAwait(false);
			if (response.IsSuccessStatusCode)
			{
				content = await response.Content.ReadAsStringAsync();
				User user = JsonConvert.DeserializeObject<User>(content);
				return user;
			}
			else
			{
				throw new Exception("Server returned response with NOT succcess status code during login post request.");
			}
		}

		private async Task<List<string>> GetListNamesFromHttpResponseMessage(HttpResponseMessage response)
		{
			var usersName = new List<string>();
			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				usersName = JsonConvert.DeserializeObject<List<string>>(content);
			}
			return usersName;
		}
	}
}
