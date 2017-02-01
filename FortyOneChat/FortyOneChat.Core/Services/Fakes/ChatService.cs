using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Core.Services.Fakes
{
    public class ChatService : IChatService
    {
        public async Task<IList<Message>> GetMessages()
        {
            var messages = new List<Message>
            {
                new Message {Id = 1, AuthorId = 1, Text = "Hi all", DateTime = DateTime.Now.AddHours(-3) },
                new Message {Id = 2, AuthorId = 2, Text = "hello", DateTime = DateTime.Now.AddHours(-4) },
                new Message {Id =3, AuthorId =1, Text="\uD83D\uDE48 \uD83D\uDE49 \uD83D\uDE49", DateTime = DateTime.Now.AddMinutes(-30) },
                new Message {Id =4, AuthorId=3, Text="Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. " },
                new Message {Id=5, AuthorId =2, Text = "\uD83D\uDE0E", DateTime = DateTime.Now }
            };

            return await Task.FromResult(messages);
        }
    }
}
