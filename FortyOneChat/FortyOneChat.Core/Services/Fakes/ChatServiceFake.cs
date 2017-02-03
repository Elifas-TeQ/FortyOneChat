using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services.Fake
{
	public class ChatServiceFake : IChatService
	{
		public async Task<List<Message>> GetChatHistory()
		{
			var messages = new List<Message>
			{
				new Message {Id = 1, Author = new User {Id = 1, Name = "Bob"}, Text = "Hi all", DateCreated = DateTime.Now.AddHours(-3) },
				new Message {Id = 2, Author = new User {Id = 2, Name = "Petya"}, Text = "hello", DateCreated = DateTime.Now.AddHours(-4) },
				new Message {Id =3, Author = new User {Id = 1, Name = "Bob"}, Text="\uD83D\uDE48 \uD83D\uDE49 \uD83D\uDE49", DateCreated = DateTime.Now.AddMinutes(-30) },
				new Message {Id =4, Author = new User {Id = 3, Name = "Krosengurg"}, Text="Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. ", DateCreated = DateTime.Now.AddMinutes(-10) },
				new Message {Id=5, Author = new User {Id = 2, Name = "Petya"}, Text = "\uD83D\uDE0E", DateCreated = DateTime.Now }
			};

			return await Task.FromResult(messages);

		}

		public Task<int> SendMessage(Message message)
		{
            return Task<bool>.Run( () => { return 1; });
		}
	}
}
