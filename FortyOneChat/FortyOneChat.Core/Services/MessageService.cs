using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FortyOneChat.Core
{
	public class MessageService : IMessageService
	{
		private string _serviceUri = @"http://10.129.68.199:8888/api/Chat";

		public async Task<MessageResponse> GetNewMessages(int lastMessageId)
		{
			if (lastMessageId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(lastMessageId));
			}

			HttpResponseMessage httpResponseMessage = await this.GetAllMessages();
			var messageResponse = new MessageResponse();

			if (httpResponseMessage.IsSuccessStatusCode)
			{
				string content = await httpResponseMessage.Content.ReadAsStringAsync();
				Message[] messages = JsonConvert.DeserializeObject<Message[]>(content);

				if ((messages.Length > lastMessageId + 1))
				{
					messageResponse.StatusCode = MessageResponseStatusCode.NewMessagess;
					messageResponse.Messages = new Message[messages.Length - lastMessageId - 1];
					Array.Copy(messages, lastMessageId + 1, messageResponse.Messages, 0, messages.Length - lastMessageId - 1);
				}
				else
				{
					messageResponse.StatusCode = MessageResponseStatusCode.NoNewMessages;
				}
			}
			else
			{
				messageResponse.StatusCode = MessageResponseStatusCode.Error;
			}

			return messageResponse;
		}

		public async Task<bool> SendNewMessage(Message message)
		{
			string content = JsonConvert.SerializeObject(message);
			var stringContent = new StringContent(content);
			var httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.PostAsync($@"{this._serviceUri}/History", stringContent);
			return response.IsSuccessStatusCode;
		}

		private async Task<HttpResponseMessage> GetAllMessages()
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync($@"{this._serviceUri}/SendMessage").ConfigureAwait(false);
			return response;
		}
	}
}
