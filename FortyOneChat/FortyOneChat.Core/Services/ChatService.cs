using System.Collections.Generic;
using FortyOneChat.Core.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace FortyOneChat.Core.Services
{
    public class ChatService : IChatService
    {
		private readonly IApplicationContext _applicationContext;

		public ChatService(IApplicationContext applicationContext)
		{
			this._applicationContext = applicationContext;
		}

		public async Task<List<Message>> GetChatHistory()
		{
			var httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.GetAsync($"{this._applicationContext.ServiceUri}/History?UserId={this._applicationContext.CurrentUser.Id}")
			                                               .ConfigureAwait(false);
			var messages = new List<Message>();
			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				messages = JsonConvert.DeserializeObject<List<Message>>(content);
			}
			return messages;
		}

		public async Task<bool> SendMessage(Message message)
		{
			string content = JsonConvert.SerializeObject(message);
			var stringContent = new StringContent(content);
			var httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.PostAsync($"{this._applicationContext.ServiceUri}/SendMessage", stringContent);
			return response.IsSuccessStatusCode;
		}
	}
}
