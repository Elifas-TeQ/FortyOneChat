using System;

namespace FortyOneChat.Core
{
	public class Message
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public int AuthorId { get; set; }
		public DateTime DateTime { get; set; }
	}
}