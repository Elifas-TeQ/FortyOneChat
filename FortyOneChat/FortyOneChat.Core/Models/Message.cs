using System;

namespace FortyOneChat.Core.Models
{
	public class Message
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public User Author { get; set; }
		public DateTime DateCreated { get; set; }
	}
}